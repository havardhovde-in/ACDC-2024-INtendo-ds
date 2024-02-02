import React, { useState } from "react";
import PlanImage from "../../Components/PlanImage/PlanImage";
import RightPanel from "../../Components/RightPanel/RightPanel";
import { Button } from "@fluentui/react-components";
import "./ProjectView.scss";
import { apiCode } from "../../Constants/Constants";
import { Order } from "../../Types/Orders";

type ProjectTypes = {
  order: Order;
};

const ProjectView: React.FC<ProjectTypes> = ({ order }) => {
  const [file, setFile] = useState<any>(null);
  const [newImage, setNewImage] = useState<any>(null);

  const url = `https://intendo-ds-api.azurewebsites.net/api/process-image?code=${apiCode}&orderId=${order.orderId}`;

  const uploadImage = async (file: File) => {
    const formData = new FormData();
    formData.append("file", file);
    console.log(formData);
    const response = await fetch(url, {
      method: "POST",
      body: formData,
    });
    const responseBlob = response.blob();

    const data = URL.createObjectURL(await responseBlob);
    setNewImage(data);
    return response;
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      setFile(e.target.files[0]);
    }
  };

  return (
    <div className="project-view">
      <h1 className="project-view__header">{order!.shippingAddress.street}</h1>
      <div className="project-view__plan">
        <PlanImage
          src={newImage ? newImage : file ? URL.createObjectURL(file) : ""}
        />
        <input onChange={(e) => handleFileChange(e)} type="file" />
        <Button appearance="primary" onClick={() => uploadImage(file)}>
          Generate
        </Button>
      </div>
      <div className="project-view__right-panel">
        <RightPanel order={order} />
      </div>
    </div>
  );
};

export default ProjectView;
