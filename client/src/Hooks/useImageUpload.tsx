import React from "react";

export default function useImageUpload() {
  const url =
    "https://intendo-ds-api.azurewebsites.net/api/ImageProcessingFunction?code=3UNxNagTqx8YUh8UExPAoZ2R__Iu7utvubIjYbxEry-xAzFu2NKqbA==";

  const uploadImage = async (file: File) => {
    const formData = new FormData();
    formData.append("file", file);
    const response = await fetch(url, {
      method: "POST",
      body: formData,
    });
    const data = await response.json();
    return data;
  };

  return { uploadImage };
}
