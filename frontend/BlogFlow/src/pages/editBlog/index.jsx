import { useState } from "react";
import ImageLoader from "../../components/imageLoader";
import "./style.css";

function EditBlogPage() {
  const [image, setImage] = useState(null);

  const handleSubmit = (event) => {
    event.preventDefault();

    console.log("Enviando imagen:", image);
  };

  return (
    <section className="editBlog-section">
      <h2>Create blog</h2>
      <form onSubmit={handleSubmit} className="editBlog-form">
        <div className="editBlog-towLine-fields">
          <label>Title</label>
          <input placeholder="Title" />
        </div>
        <div className="editBlog-towLine-fields">
          <label>Category</label>
          <input placeholder="Category" />
        </div>
        <div className="image-loader-field">
          <ImageLoader onImageSelect={setImage} />
        </div>
        <div className="editBlog-button-container">
          <button type="submit">Create Blog</button>
        </div>
      </form>
    </section>
  );
}

export default EditBlogPage;
