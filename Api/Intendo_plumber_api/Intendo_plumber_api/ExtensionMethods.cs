namespace Intendo_plumber_api;
public static class ExtensionMethods
{
  public static float RelativeToImage(this double f, int imageSize)
  {
    return (float)(imageSize * f);
  }
}
