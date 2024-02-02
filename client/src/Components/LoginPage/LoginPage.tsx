import React, { useState } from "react";
import "./LoginPage.scss";
import { Input, Button } from "@fluentui/react-components";
import Main from "../../Pages/Main/Main";

const LoginPage: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  return (
    <div className="login-page" >
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
                setIsLoggedIn(true);
              }}
              style={{ backgroundColor: "#007F0A"}}
            >
              Log In
            </Button>
          </form>
        </div>
      ) : (
        <Main />
      )}
    </div>
  );
};

export default LoginPage;
