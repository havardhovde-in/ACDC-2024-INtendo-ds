import React from "react";
import "./AppContainer.scss";
import PlanImage from "../PlanImage/PlanImage";
import RightPanel from "../RightPanel/RightPanel";

const AppContainer: React.FC = () => {
  return (
    <div className="app-container">
      <h1 className="app-container__header">Hei</h1>
      <div className="app-container__plan">
        <PlanImage />
      </div>
      <div className="app-container__right-panel">
        <RightPanel />
      </div>
    </div>
  );
};

export default AppContainer;
