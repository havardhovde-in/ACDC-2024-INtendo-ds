import React from "react";
import { Order } from "../../Types/Orders";
import "./OrderList.scss";

interface OrderListProps {
  order: Order;
  accepted: boolean;
}

const OrderList: React.FC<OrderListProps> = ({ order, accepted }) => {
  return (
    <div className={"orderList"} style={{ padding: "20px", textAlign: "left" }}>
      <div
        key={order.orderId}
        style={{
          marginBottom: "20px",
          border: "1px solid #ccc",
          borderRadius: "8px",
          padding: "15px",
          boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
        }}
      >
        <h2 style={{ marginTop: "0" }}>Order ID: {order.orderId}</h2>
        <p>
          <strong>Customer:</strong> {order.customerName} ({order.customerEmail}
          )
        </p>
        <p>
          <strong>Date:</strong>{" "}
          {new Date(order.orderDate).toLocaleDateString()}
        </p>
        <p>
          <strong>Status:</strong> {accepted ? "Accepted" : order.status}
        </p>
        <h3>Items:</h3>
        <ul>
          {order.items.map((item) => (
            <li
              key={item.productId}
              style={{ listStyle: "none", display: "flex" }}
            >
              <img
                src={item.productImage}
                alt={item.productName}
                style={{
                  width: "50px",
                  marginRight: "10px",
                  verticalAlign: "middle",
                }}
              />
              <div>
                {item.productName}
                <br />
                {item.quantity} - {item.price.toFixed(2)},-
              </div>
            </li>
          ))}
        </ul>
        <p>
          <strong>Total Amount:</strong> {order.totalAmount.toFixed(2)},-
        </p>
        <p>
          <strong>Shipping Address:</strong>{" "}
          {`${order.shippingAddress.street}, ${order.shippingAddress.city}, ${order.shippingAddress.state}, ${order.shippingAddress.postalCode}, ${order.shippingAddress.country}`}
        </p>
      </div>
    </div>
  );
};

export default OrderList;
