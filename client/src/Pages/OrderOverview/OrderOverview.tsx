import React, { useState } from "react";
import { Button, Card, CardHeader, CardPreview } from "@fluentui/react-components";
import ProjectView from "../ProjectView/ProjectView";
import "./OrderOverview.scss";
import { Order } from "../../Types/Orders";
import Plan from "../../assets/plan.png";

type MainProps = {
  orders: Order[];
};

const OrderOverview: React.FC<MainProps> = ({ orders }) => {
  const [selectedOrder, setSelectedOrder] = useState<Order>();

  React.useEffect(() => {
    console.log(orders);
  }, [orders]);

  if (!selectedOrder)
    return (
      <div className="main">
        <h1>My projects</h1>
        <div className="order-buttons">
          {orders.map((order) => {
            return (

              <Card key={order.orderId}>
                <CardPreview>
                  <img
                  src={Plan} alt="floor plan"/>
                </CardPreview>
                <CardHeader
                  header={<p>{order.shippingAddress.street}</p>}
                  />
              </Card>


              // <Button
              //   key={order.orderId}
              //   appearance="primary"
              //   onClick={() => setSelectedOrder(order)}
              // >
              //   {order!.shippingAddress.street}
              // </Button>
            );
          })}
        </div>
      </div>
    );
  if (selectedOrder) return <ProjectView order={selectedOrder} />;
  return <></>;
};

export default OrderOverview;
 