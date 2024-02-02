import React from "react";
import "./App.css";
import Home from "./Pages/Home/Home";
import Background from "./assets/background-clouds.png";
import BackgroundLeft from "./assets/lg-textured.png";
import BackgroundRight from "./assets/sm-plain.png";

function App() {
  return (
    <div className="App">
      <img className="App__background"src={Background}></img>
      <img className="App__bgimage_left"src={BackgroundLeft}></img>
      <img className="App__bgimage_right"src={BackgroundRight}></img>

     
      <Home />
    </div>
  );
}

export default App;
