import React from "react";
import PlanImage from "../../Components/PlanImage/PlanImage";
import RightPanel from "../../Components/RightPanel/RightPanel";

type ProjectTypes = {
  name: string;
};

const ProjectView: React.FC<ProjectTypes> = ({ name }) => {
  return (
    <>
      <h1 className="app-container__header">{name}</h1>
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
