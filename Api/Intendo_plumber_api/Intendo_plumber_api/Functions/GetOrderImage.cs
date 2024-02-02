using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace Intendo_plumber_api.Functions;

public class GetOrderImage
{
  private readonly Config _config;
  public GetOrderImage(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("get-order-image")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
      ILogger log)
  {
    string orderId = req.Query["orderId"];
    if (orderId == null)
    {
      return new NotFoundResult();
    }

    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Images);

    var blobClient = containerClient.GetBlobClient($"{orderId}.png");
    var blobStream = await blobClient.OpenReadAsync();
    using MemoryStream memoryStream = new();
    blobStream.CopyTo(memoryStream);
    return new FileContentResult(memoryStream.ToArray(), "image/png");
  }
}