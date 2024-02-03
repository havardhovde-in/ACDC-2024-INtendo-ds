import React from "react";
import "./RightPanel.scss";
import { Order } from "../../Types/Orders";
import OrderList from "../OrderList/OrderList";

type RightPanelTypes = {
  order: Order;
};

const RightPanel: React.FC<RightPanelTypes> = ({ order }) => {
  return (
    <div className="right-panel">
      <div className="right-panel__menu">
        <h2>Order details</h2>
        <OrderList order={order} />
        <p>{order.totalAmount.toFixed()},-</p>
      </div>
    </div>
  );
};

export default RightPanel;
