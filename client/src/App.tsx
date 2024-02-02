import React from "react";
import "./App.css";
import Home from "./Pages/Home/Home";
import { FluentProvider, teamsLightTheme } from '@fluentui/react-components';

function App() {
  return (
    <FluentProvider theme={teamsLightTheme}>
      <div className="App">
        <Home />
      </div>
    </FluentProvider>
  );
}

export default App;
