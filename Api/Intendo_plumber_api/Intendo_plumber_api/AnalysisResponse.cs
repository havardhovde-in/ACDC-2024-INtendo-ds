using System.Collections.Generic;

namespace Intendo_plumber_api;
public class AnalysisResponse
{
  public List<DetectedObject> Results { get; set; }

  public static AnalysisResponse GetTestData()
  {
    var detectedObjects = new List<DetectedObject>
      {
          // Simulating a detected object in the top-left corner
          new DetectedObject
          {
              Box = new BoundingBox
              {
                  Left = 50,    // X-coordinate of the top-left corner
                  Top = 50,     // Y-coordinate of the top-left corner
                  Width = 100,  // Width of the bounding box
                  Height = 150  // Height of the bounding box
              }
          },
          // Simulating a detected object in the center
          new DetectedObject
          {
              Box = new BoundingBox
              {
                  Left = 350,   // X-coordinate of the top-left corner
                  Top = 225,    // Y-coordinate of the top-left corner
                  Width = 100,  // Width of the bounding box
                  Height = 150  // Height of the bounding box
              }
          },
          // Simulating a detected object in the bottom-right corner
          new DetectedObject
          {
              Box = new BoundingBox
              {
                  Left = 650,   // X-coordinate of the top-left corner
                  Top = 450,    // Y-coordinate of the top-left corner
                  Width = 100,  // Width of the bounding box
                  Height = 150  // Height of the bounding box
              }
          }
      };
    return new AnalysisResponse { Results = detectedObjects };
  }
}


public class DetectedObject
{
  public string Label { get; set; }
  public float Confidence { get; set; }
  public BoundingBox Box { get; set; }
}

public class BoundingBox
{
  public float Left { get; set; }
  public float Top { get; set; }
  public float Width { get; set; }
  public float Height { get; set; }
}