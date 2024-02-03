import React from "react";
import "./PlanImage.scss";

type PlanImageProps = {
  src?: any;
};

const PlanImage: React.FC<PlanImageProps> = ({ src }) => {
  if (!src) return <></>;
  return <div className="image-wrapper" style={{maxWidth: "30rem", display:"flex", justifyContent:"center"}}><img className="plan-image" style={{width:"25rem"}} src={src} alt="plan" /></div>;
};

export default PlanImage;
