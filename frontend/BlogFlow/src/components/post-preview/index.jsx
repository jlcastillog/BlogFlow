import { useNavigate } from "react-router-dom";
import truncate from "html-truncate";
import "./style.css";

function PostPreview({ post }) {
  const navigate = useNavigate();

  const content = truncate(post.htmlContent, 90);
  const handleClick = () => {
    navigate(`/post/${post.id}`, { state: { post } });
  }
  return (
    <section className="post-preview" onClick={handleClick}>
      <h2>{post.title}</h2>
      <div dangerouslySetInnerHTML={{ __html: content }} />
    </section>
  );
}

export default PostPreview;
