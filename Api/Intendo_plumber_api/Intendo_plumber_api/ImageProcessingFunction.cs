using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PointF = SixLabors.ImageSharp.PointF;

namespace Intendo_plumber_api;


public class ImageProcessingFunction
{
  private readonly Config _config;
  public ImageProcessingFunction(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("ImageProcessingFunction")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
      ILogger log)
  {

    var predictionClient = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(_config.PredictionKey))
    {
      Endpoint = _config.PredictionEndpoint
    };

    try
    {
      using var stream = new MemoryStream();
      await req.Body.CopyToAsync(stream);
      stream.Position = 0;

      // Define the color for the brush and the width for the pen
      float penWidth = 6; // Width of the pen in pixels

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
          float objectCenterX = (float)(detectedObject.BoundingBox.Left + (detectedObject.BoundingBox.Width / 2));
          float objectCenterY = (float)(detectedObject.BoundingBox.Top + (detectedObject.BoundingBox.Height / 2));

          // Check if the object is tagged as "mainpipe"
          if (detectedObject.Tag == "Main pipe")
          {
            initialXCoord = (float)(detectedObject.BoundingBox.Left + (detectedObject.BoundingBox.Width / 2));
            initialYCoord = (float)(detectedObject.BoundingBox.Top + (detectedObject.BoundingBox.Height / 2));
            break;
          }
        }
        var size = 5;

        var squarePoints = new PointF[]{
        new(initialXCoord - size, initialYCoord - size),
        new(initialXCoord + size, initialYCoord - size),
        new(initialXCoord + size, initialYCoord + size),
        new(initialXCoord - size, initialYCoord + size)
};

        // Draw the square on the image
        image.Mutate(x => x.DrawPolygon(new DrawingOptions(), new SolidBrush(Color.Gold), penWidth, squarePoints));

        float yOffset = 10; // Vertical offset between the starting points of each line

        foreach (var detectedObject in detections.Where(x => x.Tag != "Main pipe"))
        {
          var brush = new SolidBrush(Color.Blue);

          var objectCenterX = (float)(detectedObject.BoundingBox.Left + (detectedObject.BoundingBox.Width / 2));
          var objectCenterY = (float)(detectedObject.BoundingBox.Top + (detectedObject.BoundingBox.Height / 2));


          // Calculate the starting y-coordinate for this line
          var startYCoord = initialYCoord; // + yOffset + (detections.IndexOf(detectedObject) * 5);

          // Draw the parallel line segment
          image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(initialXCoord, startYCoord), new PointF(objectCenterX, startYCoord)));

          // Draw the diverging line segment
          image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(objectCenterX, startYCoord), new PointF(objectCenterX, objectCenterY)));
        }
      }
      catch (Exception e)
      {
        return new StatusCodeResult(500);
      }
      var outStream = new MemoryStream();
      image.Save(outStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
      outStream.Position = 0;
      return new FileStreamResult(outStream, "image/png");
    }
    catch (Exception e)
    {
      log.LogError($"Exception: {e.Message}");
      return new StatusCodeResult(500);
    }
  }
}