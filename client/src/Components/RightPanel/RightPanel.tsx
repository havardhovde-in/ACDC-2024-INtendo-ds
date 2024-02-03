import React from "react";
import "./RightPanel.scss";
import { Order } from "../../Types/Orders";
import OrderList from "../OrderList/OrderList";

type RightPanelTypes = {
  order: Order;
};

const RightPanel: React.FC<RightPanelTypes> = ({ order }) => {
  return (
    <div className="right-panel  white-background">
      <div className="right-panel__menu">
        <OrderList order={order} />
      </div>
    </div>
  );
};

export default RightPanel;
