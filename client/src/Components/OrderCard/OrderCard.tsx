import React from "react";
import { Order } from "../../Types/Orders";
import { apiCode } from "../../Constants/Constants";
import "./OrderCard.scss";

type OrderCardProps = {
  order: Order;
  onClick: () => void;
};

const OrderCard: React.FC<OrderCardProps> = ({ order, onClick }) => {
  const [imageUrl, setImageUrl] = React.useState("");
  const orderImageurl = `https://intendo-ds-api.azurewebsites.net/api/get-order-image?code=${apiCode}&orderId=${order.orderId}`;

  React.useEffect(() => {
    const getOrderImage = async () => {
      const response = await fetch(orderImageurl);
      const data = await response.blob();
      const imageUrl = URL.createObjectURL(data);
      return imageUrl;
    };
    if (order) {
      getOrderImage().then((url) => {
        setImageUrl(url);
      });
    }
  }, [order, orderImageurl]);

  return (
    <div className="order-card" onClick={onClick}>
      <img className="order-card__image" src={imageUrl} alt="floor plan" />
      <h3>{order.shippingAddress.street}</h3>
      <p>Order status: {order.status}</p>
    </div>
  );
};

export default OrderCard;
