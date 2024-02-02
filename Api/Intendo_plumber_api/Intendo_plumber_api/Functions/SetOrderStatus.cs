using Intendo_plumber_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Intendo_plumber_api.Functions;

public class SetOrderStatus
{
  private readonly Config _config;
  public SetOrderStatus(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("set-order-status")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
      ILogger log)
  {
    string orderId = req.Query["orderId"];
    string status = req.Query["status"];

    if (status == null || orderId == null)
    {
      return new NotFoundResult();
    }

    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Orders);

    var blobClient = containerClient.GetBlobClient($"{orderId}.json");
    var blobStream = await blobClient.OpenReadAsync();
    using var reader = new StreamReader(blobStream);
    var jsonContent = await reader.ReadToEndAsync();
    var existingOrder = JsonConvert.DeserializeObject<Order>(jsonContent);
    existingOrder.Status = status;

    var orderString = JsonConvert.SerializeObject(existingOrder);
    await blobClient.UploadAsync(new MemoryStream(Encoding.UTF8.GetBytes(orderString)), true);
    return new OkObjectResult(existingOrder);
  }
}