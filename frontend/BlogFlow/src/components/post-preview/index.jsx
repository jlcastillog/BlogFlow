import { useNavigate } from "react-router-dom";
import truncate from "html-truncate";
import { useAuth } from "../auth";
import RoundedButton from "../buttons/roundedButton";
import "./style.css";

function PostPreview({ post }) {
  const navigate = useNavigate();
  const auth = useAuth();

  const loggedUser = auth.user;

  const content = truncate(post.htmlContent, 90);
  const onNavigatePost = () => {
    navigate(`/post/${post.id}`, { state: { post } });
  }

  const onEditPost = (event) => {
    event.stopPropagation(); 
    navigate(`/editPost/${post.id}`, { state: { post } });
  }

  return (
    <section className="post-preview" onClick={onNavigatePost}>
      {loggedUser && (<div className="edit-button-post-preview">
            <RoundedButton
              title="Edit post"
              type="edit"
              onclick={onEditPost}
            />
          </div>)}
      <h2>{post.title}</h2>
      <div dangerouslySetInnerHTML={{ __html: content }} />
    </section>
  );
}

export default PostPreview;
