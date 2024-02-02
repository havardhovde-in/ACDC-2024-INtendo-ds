import React from "react";
import PlanImage from "../../Components/PlanImage/PlanImage";
import RightPanel from "../../Components/RightPanel/RightPanel";
import { Heading } from "@chakra-ui/react";

type ProjectTypes = {
  name: string;
};

const ProjectView: React.FC<ProjectTypes> = ({ name }) => {
  return (
    <>
      <Heading className="app-container__header">{name}</Heading>
      <div className="app-container__plan">
        <PlanImage />
      </div>
      <div className="app-container__right-panel">
        <RightPanel />
      </div>
    </>
  );
};

export default ProjectView;
