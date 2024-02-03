import React, { useState } from "react";
import "./LoginPage.scss";
import { Input, Button } from "@fluentui/react-components";
import OrderOverview from "../../Pages/OrderOverview/OrderOverview";
import { apiCode } from "../../Constants/Constants";
import { Order } from "../../Types/Orders";

const LoginPage: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [orders, setOrders] = useState<Order[] | undefined>();

  const ordersUrl = `https://intendo-ds-api.azurewebsites.net/api/get-orders?code=${apiCode}`;

  const getOrders = async () => {
    const response = await fetch(ordersUrl);
    const data = await response.json();
    setOrders(data);
  };

  const handleLogin = () => {
    getOrders();
    setIsLoggedIn(true);
  };

  return (
    <div className="login-page">
      {!isLoggedIn ? (
        <div className="login-page__login">
          <h1 className="login-page__login-header">Sign in to Tubi</h1>
          <p>Log in with email</p>
          <form>
            <div className="login-page__login-form">
              <label
                className="login-page__login-form--label"
                htmlFor="username"
              >
                Username:
              </label>
              <Input type="text" id="username" />
            </div>
            <div className="login-page__login-form">
              <label
                className="login-page__login-form--label"
                htmlFor="password"
              >
                Password:
              </label>
              <Input type="password" id="password" />
            </div>
            <Button
              appearance="primary"
              className="login-page__login-button"
              onClick={() => {
                handleLogin();
              }}
              style={{ backgroundColor: "#007F0A" }}
            >
              Log In
            </Button>
          </form>
        </div>
      ) : (
        orders && <OrderOverview orders={orders} />
      )}
    </div>
  );
};

export default LoginPage;
