import React from "react";
import plan from "../../assets/plan.png";
import "./PlanImage.scss";

type PlanImageProps = {
  src?: any;
};

const PlanImage: React.FC<PlanImageProps> = ({ src }) => {
  return <img className="plan-image" src={src} alt="plan" />;
};

export default PlanImage;
