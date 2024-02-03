import React from "react";
import "./RightPanel.scss";
import { Order } from "../../Types/Orders";

type RightPanelTypes = {
  order: Order;
};

const RightPanel: React.FC<RightPanelTypes> = ({ order }) => {
  return (
    <div className="right-panel">
      <div className="right-panel__menu">
        <h2>Order details</h2>
        <ul>
          <li>Customer name: {order.customerName}</li>
          <li>Customer email: {order.customerEmail}</li>
          <li>Shipping Address: {order.shippingAddress.street}</li>
          <li>Order Status: {order.status}</li>
        </ul>
        <h2>Order items</h2>
        <ul>
          {order.items.map((item, index) => {
            return (
              <li key={index}>
                {item.productName} - {item.quantity} - {item.price.toFixed()}NOK
              </li>
            );
          })}
        </ul>
        <p>{order.totalAmount.toFixed()},-</p>
      </div>
    </div>
  );
};

export default RightPanel;
