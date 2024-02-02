using Intendo_plumber_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intendo_plumber_api.Functions;

public class CreateOrderFunction
{
  private readonly Config _config;
  public CreateOrderFunction(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("create-order")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
      ILogger log)
  {
    var createOrderDto = JsonConvert.DeserializeObject<Order>(await req.ReadAsStringAsync());
    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Orders);

    var blobs = containerClient.GetBlobs();
    var orderId = int.Parse(blobs.Max(b => b.Name).Replace(".json", ""));
    createOrderDto.OrderId = orderId.ToString();
    try
    {
      var orderString = JsonConvert.SerializeObject(createOrderDto);
      await containerClient.UploadBlobAsync($"{orderId + 1}.json", new MemoryStream(Encoding.UTF8.GetBytes(orderString)));

      return new OkObjectResult(createOrderDto);
    }
    catch (Exception e)
    {
      log.LogError($"Exception: {e.Message}");
      return new StatusCodeResult(500);
    }
  }
}