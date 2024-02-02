import React, { useState } from "react";
import "./AppContainer.scss";
import { Input, Button } from "@fluentui/react-components";
import Main from "../../Pages/Main/Main";
import { apiCode } from "../../Constants/Constants";
import { Order } from "../../Types/Orders";

const AppContainer: React.FC = () => {
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
    <div className="app-container">
      {!isLoggedIn ? (
        <div className="app-container__login">
          <h1 className="app-container__login-header">Log In</h1>
          <form>
            <div className="app-container__login-form">
              <label
                className="app-container__login-form--label"
                htmlFor="username"
              >
                Username:
              </label>
              <Input type="text" id="username" />
            </div>
            <div className="app-container__login-form">
              <label
                className="app-container__login-form--label"
                htmlFor="password"
              >
                Password:
              </label>
              <Input type="password" id="password" />
            </div>
            <Button
              appearance="primary"
              className="app-container__login-button"
              onClick={() => {
                handleLogin();
              }}
            >
              Log In
            </Button>
          </form>
        </div>
      ) : (
        orders && <Main orders={orders} />
        // <Main orders={orders} />
      )}
    </div>
  );
};

export default AppContainer;
