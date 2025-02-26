import { useState } from "react";
import { useLocation } from "react-router-dom";
import "./style.css";

function PostPage() {
  const location = useLocation();
  const [post, setPost] = useState(location.state.post);
  return (
    <section className="post-section-container">
      <div className="post-content">
        <h1>{post.title}</h1>
        <div dangerouslySetInnerHTML={{ __html: post.htmlContent }} />
      </div>
    </section>
  );
}

export default PostPage;
