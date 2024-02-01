import React from "react";
import plan from "../../assets/plan.png";
import "./PlanImage.scss";

const PlanImage: React.FC = () => {
  return <img className="plan-image" src={plan} alt="plan" />;
};

export default PlanImage;
