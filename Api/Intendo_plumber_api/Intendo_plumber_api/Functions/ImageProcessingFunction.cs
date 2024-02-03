using Intendo_plumber_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointF = SixLabors.ImageSharp.PointF;

namespace Intendo_plumber_api.Functions;

public class ImageProcessingFunction
{
  private readonly Config _config;
  public ImageProcessingFunction(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("process-image")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
      ILogger log)
  {
    var predictionClient = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(_config.PredictionKey))
    {
      Endpoint = _config.PredictionEndpoint
    };

    string orderId = req.Query["orderId"];

    try
    {
      var file = req.Form.Files.FirstOrDefault();
      var stream = file.OpenReadStream();
      stream.Position = 0;

      // Define the color for the brush and the width for the pen
      float penWidth = 3; // Width of the pen in pixels

      using Image image = Image.Load(stream);

      float yCoord = image.Height / 2;
      float xCoord = image.Width;
      try
      {
        stream.Position = 0;
        var aiResponse = await predictionClient.DetectImageAsync(_config.ProjectId, _config.IterationName, stream);

        var predictionDetections = aiResponse.Predictions.ToList();

        var detections = predictionDetections.Where(x => x.Probability > .90).Select(x => new DetectedObject
        {
          BoundingBox = new BoundingBox
          {
            Left = x.BoundingBox.Left.RelativeToImage(image.Width),
            Top = x.BoundingBox.Top.RelativeToImage(image.Height),
            Width = x.BoundingBox.Width.RelativeToImage(image.Width),
            Height = x.BoundingBox.Height.RelativeToImage(image.Height)
          },
          Tag = x.TagName
        }).ToList();

        float initialXCoord = image.Width;
        float initialYCoord = image.Height / 2;

        foreach (var detectedObject in detections)
        {
          float objectCenterX = (float)(detectedObject.BoundingBox.Left + detectedObject.BoundingBox.Width / 2);
          float objectCenterY = (float)(detectedObject.BoundingBox.Top + detectedObject.BoundingBox.Height / 2);

          // Check if the object is tagged as "mainpipe"
          if (detectedObject.Tag == "Main pipe")
          {
            initialXCoord = (float)(detectedObject.BoundingBox.Left + detectedObject.BoundingBox.Width / 2);
            initialYCoord = (float)(detectedObject.BoundingBox.Top + detectedObject.BoundingBox.Height / 2);
            break;
          }
        }
        var size = 5;


        var pipeProduct = Constants.PipeProduct;
        var muffProduct = Constants.MuffProduct;
        var orderItems = new List<OrderItem>();

        float metersOfPipe = 0;
        var numberOfMuffs = 0;

        foreach (var detectedObject in detections.Where(x => x.Tag != "Main pipe"))
        {
          var brush = new SolidBrush(Color.Blue);

          var objectCenterX = (float)(detectedObject.BoundingBox.Left + detectedObject.BoundingBox.Width / 2);
          var objectCenterY = (float)(detectedObject.BoundingBox.Top + detectedObject.BoundingBox.Height / 2);

          var horizontalLineLength = Math.Abs(initialXCoord - objectCenterX);
          var verticalLineLength = Math.Abs(initialYCoord - objectCenterY);

          var combinedLength = horizontalLineLength + verticalLineLength;
          metersOfPipe += (combinedLength / 100);
          numberOfMuffs += 2;
          image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(initialXCoord - 2, initialYCoord - 2), new PointF(objectCenterX - 2, initialYCoord - 2)));
          image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(objectCenterX - 2, initialYCoord - 2), new PointF(objectCenterX - 2, objectCenterY - 2)));

          if (Constants.Tags.DoesTagHaveHotWater(detectedObject.Tag))
          {
            brush = new SolidBrush(Color.Red);
            combinedLength += horizontalLineLength + verticalLineLength;
            metersOfPipe += (combinedLength / 100);
            numberOfMuffs += 2;
            image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(initialXCoord + 3, initialYCoord + 3), new PointF(objectCenterX + 3, initialYCoord + 3)));
            image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(objectCenterX + 3, initialYCoord + 3), new PointF(objectCenterX + 3, objectCenterY + 3)));
          }
          var destinationSquare = new PointF[]{
            new(objectCenterX - size, objectCenterY - size),
            new(objectCenterX + size, objectCenterY - size),
            new(objectCenterX + size, objectCenterY + size),
            new(objectCenterX - size, objectCenterY + size)
          };

          // Draw the square on the image
          image.Mutate(x => x.DrawPolygon(new DrawingOptions(), new SolidBrush(Color.Gold), 10, destinationSquare));
        }
        //Starting point
        var squarePoints = new PointF[]{
          new(initialXCoord - size, initialYCoord - size),
          new(initialXCoord + size, initialYCoord - size),
          new(initialXCoord + size, initialYCoord + size),
          new(initialXCoord - size, initialYCoord + size)
        };

        // Draw the square on the image
        image.Mutate(x => x.DrawPolygon(new DrawingOptions(), new SolidBrush(Color.Gold), 10, squarePoints));

        orderItems.Add(new OrderItem
        {
          ProductId = pipeProduct.ProductId,
          ProductName = pipeProduct.ProductName,
          ProductImage = pipeProduct.ImageUrl,
          Quantity = $"{metersOfPipe.ToString("0.00")} m",
          Price = pipeProduct.UnitPrice * (metersOfPipe / 2)
        });

        orderItems.Add(new OrderItem
        {
          ProductId = muffProduct.ProductId,
          ProductName = muffProduct.ProductName,
          ProductImage = muffProduct.ImageUrl,
          Quantity = $"{numberOfMuffs} stk",
          Price = muffProduct.UnitPrice * numberOfMuffs
        });

        await UpdateOrder(orderId, orderItems);
      }
      catch (Exception e)
      {
        return new StatusCodeResult(500);
      }
      var outStream = new MemoryStream();
      image.Save(outStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
      outStream.Position = 0;

      var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
      var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Images);
      var blobClient = containerClient.GetBlobClient($"{orderId}.png");
      if (blobClient.Exists())
      {
        blobClient.Upload(outStream, true);
      }
      else
      {
        await containerClient.UploadBlobAsync($"{orderId}.png", outStream);
      }

      outStream.Position = 0;
      return new FileStreamResult(outStream, "image/png");
    }
    catch (Exception e)
    {
      log.LogError($"Exception: {e.Message}");
      return new StatusCodeResult(500);
    }
  }

  private async Task UpdateOrder(string orderId, List<OrderItem> orderItems)
  {
    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Orders);

    var blobClient = containerClient.GetBlobClient($"{orderId}.json");
    var blobStream = await blobClient.OpenReadAsync();
    using var reader = new StreamReader(blobStream);
    var jsonContent = await reader.ReadToEndAsync();
    var existingOrder = JsonConvert.DeserializeObject<Order>(jsonContent);
    existingOrder.Items = orderItems;
    existingOrder.OrderId = orderId;
    existingOrder.Status = "Open";
    existingOrder.TotalAmount = (decimal)existingOrder.Items.Sum(i => i.Price);
    var orderString = JsonConvert.SerializeObject(existingOrder);
    await blobClient.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes(orderString)), true);

  }
}