using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;

namespace Intendo_plumber_api;
public static class TestDataGenerator
{
  public static AnalysisResponse GetTestData()
  {
    var random = new Random();
    var detectedObjects = new List<DetectedObject>();

    // List of possible tags
    var tags = new List<string> { "shower", "bath tub", "toilet", "sink", "mainpipe" };
    var colors = new List<Color> { Color.Red, Color.Green, Color.Blue };

    for (int i = 0; i < 3; i++) // Generate 3 random objects
    {
      detectedObjects.Add(new DetectedObject
      {
        BoundingBox = new BoundingBox
        {
          Left = random.Next(0, 700), // Assuming image width of 800
          Top = random.Next(0, 500),  // Assuming image height of 600
          Width = random.Next(50, 150),
          Height = random.Next(50, 150)
        },
        Color = colors[random.Next(colors.Count)],
        Tag = tags[random.Next(tags.Count)] // Randomly select a tag
      });
    }

    return new AnalysisResponse { Results = detectedObjects };
  }
}