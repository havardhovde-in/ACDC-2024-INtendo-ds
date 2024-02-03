import React from "react";
import "./App.css";
import Background from "./assets/background-clouds.png";
import BackgroundLeft from "./assets/lg-textured.png";
import BackgroundRight from "./assets/sm-plain.png";
import LoginPage from "./Components/LoginPage/LoginPage";

function App() {
  return (
    <div className="App" style={{color:"#424242"}}>
      <img className="App__background" src={Background} alt="background"></img>
      <img
        className="App__bgimage_left"
        src={BackgroundLeft}
        alt="left background"
      ></img>
      <img
        className="App__bgimage_right"
        src={BackgroundRight}
        alt="right-background"
      ></img>

      <LoginPage />
    </div>
  );
}

export default App;
