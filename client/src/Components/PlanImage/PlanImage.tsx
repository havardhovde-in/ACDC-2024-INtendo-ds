import React from "react";
import "./PlanImage.scss";

type PlanImageProps = {
  src?: any;
};

const PlanImage: React.FC<PlanImageProps> = ({ src }) => {
  if (!src) return <></>;
  return <img className="plan-image" src={src} alt="plan" />;
};

export default PlanImage;
