import React, { useState } from "react";
import PlanImage from "../../Components/PlanImage/PlanImage";
import RightPanel from "../../Components/RightPanel/RightPanel";

type ProjectTypes = {
  name: string;
};

const ProjectView: React.FC<ProjectTypes> = ({ name }) => {
  const [file, setFile] = useState<any>(null);
  const [newImage, setNewImage] = useState<any>(null);

  const url =
    "https://intendo-ds-api.azurewebsites.net/api/ImageProcessingFunction?code=3UNxNagTqx8YUh8UExPAoZ2R__Iu7utvubIjYbxEry-xAzFu2NKqbA==";

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
    <>
      <h1 className="app-container__header">{name}</h1>
      <div className="app-container__plan">
        <PlanImage src={newImage ? newImage : file} />
        <input onChange={(e) => handleFileChange(e)} type="file" />
        <img className="imagepreview" src={file} alt="uploaded" />
        <Button colorScheme="blue" onClick={() => uploadImage(file)}>
          Send
        </Button>
      </div>
      <div className="app-container__right-panel">
        <RightPanel />
      </div>
    </>
  );
};

export default ProjectView;
