namespace Intendo_plumber_api.Models;
public class Product
{
  public string ProductId { get; set; }
  public string ProductName { get; set; }
  public float UnitPrice { get; set; }
  public bool IsPipe { get; set; }
  public int Length { get; set; }
  public string ImageUrl { get; set; }
}
