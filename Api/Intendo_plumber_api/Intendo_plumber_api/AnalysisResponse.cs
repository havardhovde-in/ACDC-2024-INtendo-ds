using SixLabors.ImageSharp;
using System.Collections.Generic;

public class AnalysisResponse
{
  public List<DetectedObject> Results { get; set; }
}

public class DetectedObject
{
  public Color Color { get; set; }
  public string Tag { get; set; }
  public BoundingBox BoundingBox { get; set; }
}

public class BoundingBox
{
  public float Left { get; set; }
  public float Top { get; set; }
  public float Width { get; set; }
  public float Height { get; set; }
}