import React, { useState } from "react";
import { Button } from "@fluentui/react-components";
import ProjectView from "../ProjectView/ProjectView";
import "./Main.scss";

const Main = () => {
  const [selectedProject, setSelectedProject] = useState<string>("");

  if (!selectedProject)
    return (
      <div className="main">
        <h1>Select an order</h1>
        <div className="order-buttons">
          <Button
            appearance="primary"
            onClick={() => setSelectedProject("Rørveien 1")}
          >
            Order 1
          </Button>{" "}
          <Button
            appearance="primary"
            onClick={() => setSelectedProject("Rørveien 1")}
          >
            Order 2
          </Button>
        </div>
      </div>
    );
  return <ProjectView name={selectedProject} />;
};

export default Main;
