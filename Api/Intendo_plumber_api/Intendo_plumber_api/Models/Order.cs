using System;
using System.Collections.Generic;

namespace Intendo_plumber_api.Models;
public class Order
{
  public string OrderId { get; set; }
  public string CustomerEmail { get; set; }
  public string CustomerName { get; set; }
  public DateTime OrderDate { get; set; }
  public List<OrderItem> Items { get; set; }
  public decimal TotalAmount { get; set; }
  public ShippingAddress ShippingAddress { get; set; }
}

public class OrderItem
{
  public string ProductId { get; set; }
  public string ProductName { get; set; }
  public int Quantity { get; set; }
  public decimal UnitPrice { get; set; }
}

public class ShippingAddress
{
  public string Street { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string PostalCode { get; set; }
  public string Country { get; set; }
}