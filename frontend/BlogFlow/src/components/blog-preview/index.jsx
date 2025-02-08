import { useNavigate } from "react-router-dom";
import "./style.css";

function BlogPreview({ blog }) {

  const navigate = useNavigate();

  const handleOnClick = () => {
    navigate(`/blog/${blog.id}`, { state: { blog } });
  }

  return (
    <div className="blog-preview" onClick={handleOnClick}>
      <img src={`data:image/jpeg;base64,${blog.image}`} alt="Imagen del blog" className="blog-preview-image" />
      <div className="blog-preview-content">
        <h2 className="blog-preview-title">{blog.title}</h2>
        <p className="blog-preview-author">{blog.author}</p>
        <span className="blog-preview-category">{blog.category}</span>
      </div>
    </div>
  );
}

export default BlogPreview;
