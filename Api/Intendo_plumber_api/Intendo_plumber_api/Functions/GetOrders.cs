using Intendo_plumber_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Intendo_plumber_api.Functions;

public class GetOrders
{
  private readonly Config _config;
  public GetOrders(IOptions<Config> config)
  {
    _config = config.Value;
  }

  [FunctionName("get-orders")]
  public async Task<IActionResult> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
      ILogger log)
  {
    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Orders);

    var blobs = containerClient.GetBlobs();
    try
    {
      var orders = new List<Order>();
      foreach (var blob in blobs)
      {
        var blobClient = containerClient.GetBlobClient(blob.Name);
        var blobStream = await blobClient.OpenReadAsync();
        using var reader = new StreamReader(blobStream);
        var jsonContent = await reader.ReadToEndAsync();
        orders.Add(JsonConvert.DeserializeObject<Order>(jsonContent));
      }

      return new OkObjectResult(orders);
    }
    catch (Exception e)
    {
      log.LogError($"Exception: {e.Message}");
      return new StatusCodeResult(500);
    }
  }
}