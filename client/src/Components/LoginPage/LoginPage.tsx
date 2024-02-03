import React, { useState } from "react";
import "./LoginPage.scss";
import {
  Input,
  Button,
  Dialog,
  DialogSurface,
} from "@fluentui/react-components";
import { apiCode } from "../../Constants/Constants";
import { Order } from "../../Types/Orders";
import OrderOverview from "../../Pages/OrderOverview/OrderOverview";

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
        <Dialog open={!isLoggedIn}>
          <DialogSurface>
            <div className="login-page__login">
              <div className="login-page__login-header">
                <h1>Sign in to Tubi</h1>
                <p>Log in with email</p>
              </div>
              <form className="login-page__login-form-container">
                <div className="login-page__login-form">
                  <label
                    className="login-page__login-form--label"
                    htmlFor="username"
                  ></label>
                  <Input
                    type="text"
                    id="username"
                    size="large"
                    placeholder="Username"
                    style={{ width: "80%" }}
                  />
                </div>
                <div className="login-page__login-form">
                  <label
                    className="login-page__login-form--label"
                    htmlFor="password"
                  ></label>
                  <Input
                    type="password"
                    id="password"
                    size="large"
                    placeholder="Password"
                    style={{ width: "80%" }}
                  />
                </div>
                <Button
                  appearance="primary"
                  className="login-page__login-button"
                  onClick={() => {
                    handleLogin();
                  }}
                  style={{
                    backgroundColor: "#007F0A",
                    marginTop: "20px",
                  }}
                >
                  Log In
                </Button>
              </form>
            </div>
          </DialogSurface>
        </Dialog>
      ) : (
        orders && <OrderOverview orders={orders} />
      )}
    </div>
  );
};

export default LoginPage;
