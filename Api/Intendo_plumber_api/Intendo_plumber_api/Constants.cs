using Intendo_plumber_api.Models;

namespace Intendo_plumber_api;
public static class Constants
{
  public static class Tags
  {
    public static readonly string MainPipe = "Main Pipe";
    public static readonly string BathTub = "BathTub";
    public static readonly string Sink = "BathTub";
    public static readonly string Toilet = "Toilet";
    public static readonly string Shower = "Shower";
  }
  public static class Containers
  {
    public static readonly string Orders = "orders";
    public static readonly string Images = "images";

  }

  public static Product PipeProduct = new()
  {
    ProductName = "Copper pipe",
    ProductId = "34509-2931-1",
    UnitPrice = 5,
    ImageUrl = "https://productimages.biltema.com/v1/image/product/medium/2000018550/2"
  };

  public static Product MuffProduct = new()
  {
    ProductName = "Muff",
    ProductId = "4235-32-345",
    UnitPrice = 20,
    ImageUrl = "https://productimages.biltema.com/v1/image/imagebyfilename/86-961_xl_1.jpg"
  };
}
