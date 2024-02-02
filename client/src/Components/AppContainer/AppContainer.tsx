import React, { useState } from "react";
import "./AppContainer.scss";
import { Input, Button, Text } from "@fluentui/react-components";
import Main from "../../Pages/Main/Main";

const AppContainer: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  return (
    <div className="app-container">
      {!isLoggedIn ? (
        <div className="app-container__login">
          <h1 className="app-container__login-header">
            Log In
          </h1>
          <form>
            <div className="app-container__login-form">
              <label htmlFor="username">Username</label>
              <Input type="text" id="username" />
            </div>
            <div className="app-container__login-form">
              <label htmlFor="password">Password</label>
              <Input type="password" id="password" />
            </div>
            <Button
              appearance="primary"
              className="app-container__login-button"
              onClick={() => {
                setIsLoggedIn(true);
              }}
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

export default AppContainer;
