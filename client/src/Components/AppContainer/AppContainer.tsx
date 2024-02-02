import React, { useState } from "react";
import "./AppContainer.scss";
import { Input, Button, Heading } from "@chakra-ui/react";
import Main from "../../Pages/Main/Main";

const AppContainer: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  return (
    <div className="app-container">
      {!isLoggedIn ? (
        <div className="app-container__login">
          <Heading className="app-container__login-header" as="h1">
            Log In
          </Heading>
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
              colorScheme="blue"
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
