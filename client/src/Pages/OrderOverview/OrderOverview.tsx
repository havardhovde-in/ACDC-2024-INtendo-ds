import React, { useState } from "react";
import ProjectView from "../ProjectView/ProjectView";
import "./OrderOverview.scss";
import { Order } from "../../Types/Orders";
import Avatar from "../../assets/Sofie.png";
import OrderCard from "../../Components/OrderCard/OrderCard";

type MainProps = {
  orders: Order[];
};

const OrderOverview: React.FC<MainProps> = ({ orders }) => {
  const [selectedOrder, setSelectedOrder] = useState<Order>();

  if (!selectedOrder)
    return (
      <div className="main">
        <img className="orderOverview_avatar" src={Avatar} alt="avatar"></img>

        <h1>My projects</h1>
        <div className="order-buttons">
          {orders.map((order) => {
            return (
              <OrderCard
                key={order.orderId}
                order={order}
                onClick={() => setSelectedOrder(order)}
              />
            );
          })}
        </div>
      </div>
    );
  if (selectedOrder) return <ProjectView order={selectedOrder} />;
  return <></>;
};

export default OrderOverview;
