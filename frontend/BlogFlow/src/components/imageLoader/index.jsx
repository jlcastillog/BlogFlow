import { useState } from "react";
import "./style.css";

const ImageLoader = ({ onImageSelect, image, imageUrl }) => {
  const [imagePreview, setImagePreview] = useState(imageUrl);

  if (image) {
    console.log(image);
    const reader = new FileReader();
      reader.onloadend = () => {
        setImagePreview(reader.result);
      };
      reader.readAsDataURL(image);
  }

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setImagePreview(reader.result);
        onImageSelect(file); // Envía el archivo al componente padre si es necesario
      };
      reader.readAsDataURL(file);
    }
  };

  return (
    <div className="image-loader">
      <input 
        type="file" 
        accept="image/*" 
        onChange={handleImageChange} 
      />
      {imagePreview && (
        <img 
          src={imagePreview} 
          alt="Vista previa" 
          className="image-preview"
        />
      )}
    </div>
  );
};

export default ImageLoader;