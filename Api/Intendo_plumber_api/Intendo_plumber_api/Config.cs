using System;

namespace Intendo_plumber_api;
public class Config
{
  public string PredictionKey { get; set; }
  public Guid ProjectId { get; set; }
  public string IterationName { get; set; }
  public string PredictionEndpoint { get; set; }
}