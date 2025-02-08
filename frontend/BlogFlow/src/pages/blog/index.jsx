import { useParams, useLocation } from "react-router-dom";
import blogsSampleData from "../../assets/data/blogsSample.json";
import "./style.css";

function BlogPage() {
  const { id } = useParams();
  const location = useLocation();

  let blog = null;

  if (location?.state?.blog) {
    blog = location.state.blog;
  } else {
    blog = blogsSampleData.find((blog) => blog.id === id);
  }

  return (
    <section className="blog-section">
      <h1>{blog.title}</h1>
      <div className="blog-info">
        <div className="blog-text-container">
          <p className="blog-author">By {blog.author}</p>
          <p className="blog-category">{blog.category}</p>
        </div>
        <div className="blog-image-container">
          <img
            src={`data:image/jpeg;base64,${blog.image}`}
            alt="Imagen del blog"
            className="blog-image"
          />
        </div>
      </div>
    </section>
  );
}

export default BlogPage;
