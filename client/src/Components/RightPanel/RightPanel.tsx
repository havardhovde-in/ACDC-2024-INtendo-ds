import React from "react";
import { Avatar } from "@fluentui/react-components";
import "./RightPanel.scss";

const RightPanel: React.FC = () => {
  return (
    <div className="right-panel">
      <div className="right-panel__menu">
        <Avatar name="Fyr Pilsesen" />
      </div>
    </div>
  );
};

export default RightPanel;
