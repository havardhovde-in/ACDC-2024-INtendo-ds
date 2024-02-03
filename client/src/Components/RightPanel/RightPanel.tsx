import React from "react";
import "./RightPanel.scss";
import { Order } from "../../Types/Orders";
import OrderList from "../OrderList/OrderList";
import { Button } from "@fluentui/react-components";
import { apiCode } from "../../Constants/Constants";

type RightPanelTypes = {
  order: Order;
};

///Håvard, kan du legge til funksjonalitet her som endrer order state til accepted?
const RightPanel: React.FC<RightPanelTypes> = ({ order }) => {
  const [accepted, setAccepted] = React.useState<boolean>(false);
  const updateOrderUrl = `https://intendo-ds-api.azurewebsites.net/api/set-order-status?code=${apiCode}&orderId=${order.orderId}&status=Accepted`;

  const updateOrder = async () => {
    const response = await fetch(updateOrderUrl, {
      method: "GET",
    });
    const data = await response.json();
    console.log(data);
    setAccepted(true);
  };

  return (
    <div className="right-panel  white-background">
      <div className="right-panel__menu">
        <OrderList accepted={accepted} order={order} />
      </div>
      <Button onClick={updateOrder} appearance="primary">
        Accept
      </Button>
    </div>
  );
};

export default RightPanel;
