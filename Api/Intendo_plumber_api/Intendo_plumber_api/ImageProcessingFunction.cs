using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Intendo_plumber_api;


public static class ImageProcessingFunction
{
  [FunctionName("ImageProcessingFunction")]
  public static async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
      ILogger log)
  {
    log.LogInformation("C# HTTP trigger function processed a request.");

    try
    {
      using var stream = new MemoryStream();
      await req.Body.CopyToAsync(stream);
      stream.Position = 0;

      // Define the color for the brush and the width for the pen
      Color brushColor = Color.Red;
      float penWidth = 6; // Width of the pen in pixels

      // Create a solid brush
      var brush = new SolidBrush(brushColor);

      using Image image = Image.Load(stream);

      var yCoord = image.Height / 2;
      var xCoord = image.Width;

      // Dummy list of DetectedObjects - replace with actual objects from AI Builder response
      var response = AnalysisResponse.GetTestData();

      // Iterate through each detected object and draw a line
      foreach (var detectedObject in response.Results)
      {
        float centerX = detectedObject.Box.Left + (detectedObject.Box.Width / 2);
        float centerY = detectedObject.Box.Top + (detectedObject.Box.Height / 2);

        image.Mutate(x => x.DrawLine(new DrawingOptions(), brush, penWidth, new PointF(xCoord, yCoord), new PointF(centerX, centerY)));
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