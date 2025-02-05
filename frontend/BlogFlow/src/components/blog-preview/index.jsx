import "./style.css";

function BlogPreview({ blog }) {
  return (
    <div className="blog-preview">
      <img src={blog.content} alt="Imagen del blog" className="blog-image" />
      <div className="blog-content">
        <h2 className="blog-title">{blog.title}</h2>
        <p className="blog-author">{blog.author}</p>
        <span className="blog-category">{blog.category}</span>
      </div>
    </div>
  );
}

export default BlogPreview;
