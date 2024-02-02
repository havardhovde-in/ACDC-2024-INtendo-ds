using Intendo_plumber_api.Models; // Import models used within the Intendo plumber API.
using Microsoft.AspNetCore.Http; // Import for handling HTTP requests and responses.
using Microsoft.AspNetCore.Mvc; // Import for action result types in controllers.
using Microsoft.Azure.WebJobs; // Import for Azure Functions attributes and bindings.
using Microsoft.Azure.WebJobs.Extensions.Http; // Import for triggering functions with HTTP requests.
using Microsoft.Extensions.Logging; // Import for logging within the function.
using Microsoft.Extensions.Options; // Import for accessing application settings.
using Newtonsoft.Json; // Import for JSON serialization and deserialization.
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intendo_plumber_api.Functions;

public class CreateOrderFunction
{
  private readonly Config _config; // Configuration object to hold app settings.
  public CreateOrderFunction(IOptions<Config> config)
  {
    _config = config.Value; // Initialize configuration from app settings.
  }

  // Defines a function named "create-order" that responds to HTTP POST requests.
  [FunctionName("create-order")]
  public async Task<IActionResult> Run(
      // Configures the HTTP trigger with anonymous authorization and no specific route.
      [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
      ILogger log) // Logger instance for logging.
  {
    // Deserialize the JSON body of the request into an Order object.
    var createOrderDto = JsonConvert.DeserializeObject<Order>(await req.ReadAsStringAsync());
    // Create a BlobServiceClient using the storage account connection string from configuration.
    var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(_config.StorageAccountConnectionstring);
    // Get a reference to the blob container for orders.
    var containerClient = blobServiceClient.GetBlobContainerClient(Constants.Containers.Orders);

    // Retrieve all blobs in the container and calculate the next order ID.
    var blobs = containerClient.GetBlobs();
    var orderId = int.Parse(blobs.Max(b => b.Name).Replace(".json", ""));
    createOrderDto.OrderId = orderId.ToString(); // Set the order ID on the DTO.

    try
    {
      // Serialize the order DTO to a JSON string.
      var orderString = JsonConvert.SerializeObject(createOrderDto);
      // Upload the serialized order to the blob container, incrementing the order ID for the filename.
      await containerClient.UploadBlobAsync($"{orderId + 1}.json", new MemoryStream(Encoding.UTF8.GetBytes(orderString)));

      // Return the created order DTO as a success response.
      return new OkObjectResult(createOrderDto);
    }
    catch (Exception e)
    {
      // Log the exception and return a 500 Internal Server Error response.
      log.LogError($"Exception: {e.Message}");
      return new StatusCodeResult(500);
    }
  }
}
