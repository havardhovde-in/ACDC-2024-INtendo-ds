import React, { useState } from "react";
import { Button } from "@fluentui/react-components";
import ProjectView from "../ProjectView/ProjectView";
import "./OrderOverview.scss";
import { Order } from "../../Types/Orders";

type MainProps = {
  orders: Order[];
};

const OrderOverview: React.FC<MainProps> = ({ orders }) => {
  const [selectedOrder, setSelectedOrder] = useState<Order>();

  if (!selectedOrder)
    return (
      <div className="main">
        <h1>Select an order</h1>
        <div className="order-buttons">
          {orders.map((order) => {
            return (
              <Button
                key={order.orderId}
                appearance="primary"
                onClick={() => setSelectedOrder(order)}
              >
                {order!.shippingAddress.street}
              </Button>
            );
          })}
        </div>
      </div>
    );
  if (selectedOrder) return <ProjectView order={selectedOrder} />;
  return <></>;
};

export default OrderOverview;
