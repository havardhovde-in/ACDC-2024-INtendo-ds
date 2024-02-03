import React from "react";
import "./RightPanel.scss";
import { Order } from "../../Types/Orders";
import OrderList from "../OrderList/OrderList";
import { Button } from "@fluentui/react-components";

type RightPanelTypes = {
  order: Order;
};


///Håvard, kan du legge til funksjonalitet her som endrer order state til accepted?
const RightPanel: React.FC<RightPanelTypes> = ({ order }) => {
  return (
    <div className="right-panel  white-background">
      <div className="right-panel__menu">
        <OrderList order={order} />
      </div>
      <Button appearance="primary" >
          Analyze
        </Button>
            </div>
  );
};

export default RightPanel;
