import React from "react";
import "./App.css";
import Home from "./Pages/Home/Home";
import Background from "./assets/background-clouds.png";
import BackgroundLeft from "./assets/lg-textured.png";
import BackgroundRight from "./assets/sm-plain.png";

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

      <Home />
    </div>
  );
}

export default App;
