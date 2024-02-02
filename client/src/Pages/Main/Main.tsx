import React, { useState } from "react";
import { Button } from "@fluentui/react-components";
import ProjectView from "../ProjectView/ProjectView";

const Main = () => {
  const [selectedProject, setSelectedProject] = useState<string>("");

  if (!selectedProject)
    return (
      <div>
        <h1>Select a project</h1>
        <Button
          appearance="primary"
          onClick={() => setSelectedProject("Rørveien 1")}
        >
          Select project
        </Button>
      </div>
    );
  return <ProjectView name={selectedProject} />;
};

export default Main;
