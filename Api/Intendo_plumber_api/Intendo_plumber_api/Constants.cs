using Intendo_plumber_api.Models;
using System.Linq;

namespace Intendo_plumber_api;
public static class Constants
{
  public static class Tags
  {
    public static readonly string MainPipe = "Main Pipe";
    public static readonly string BathTub = "Bath tub";
    public static readonly string Sink = "Sink";
    public static readonly string Toilet = "Toilet";
    public static readonly string Shower = "Shower";

    public static bool DoesTagHaveHotWater(string tag) => (new string[] { Sink, Shower, BathTub }).Contains(tag);
  }
  public static class Containers
  {
    public static readonly string Orders = "orders";
    public static readonly string Images = "images";
  }

  public static Product PipeProduct = new()
  {
    ProductName = "Copper Piping, raw, 2000 mm",
    ProductId = "34509-2931-1",
    UnitPrice = 139,
    ImageUrl = "https://productimages.biltema.com/v1/image/product/medium/2000018550/2"
  };

  public static Product MuffProduct = new()
  {
    ProductName = "Angled connector with rubber seal, 90°, 2 muff",
    ProductId = "4235-32-345",
    UnitPrice = 20,
    ImageUrl = "https://productimages.biltema.com/v1/image/imagebyfilename/86-961_xl_1.jpg"
  };
}
