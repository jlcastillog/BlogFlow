import { useNavigate } from "react-router-dom";
import { useState } from "react";
import truncate from "html-truncate";
import { useAuth } from "../auth";
import RoundedButton from "../buttons/roundedButton";
import { deletePost } from "../../services/post/postService";
import ErrorPanel from "../../components/error";
import "./style.css";

function PostPreview({ post, onRemovedPost }) {
  const navigate = useNavigate();
  const [error, setError] = useState(null);
  const auth = useAuth();

  const loggedUser = auth.user;

  const content = truncate(post.htmlContent, 90);
  const onNavigatePost = () => {
    navigate(`/post/${post.id}`, { state: { post } });
  };

  const onEditPost = (event) => {
    event.stopPropagation();
    navigate(`/editPost/${post.id}`, { state: { post } });
  };

  const onRemovePost = async (event) => {
    event.stopPropagation();
    try {
      event.preventDefault();
      await deletePost(post.id);
      onRemovedPost(post);
    } catch (err) {
      setError(err.message);
      if (err.message === "Refresh token failed") {
        auth.resetUser();
      }
    }
  };

  return (
    <section className="post-preview" onClick={onNavigatePost}>
      {loggedUser && (
        <div className="buttons-post-preview">
          <div className="edit-button-post-preview">
            <RoundedButton title="Edit post" type="edit" onclick={onEditPost} />
          </div>
          <div className="remove-button-post-preview">
            <RoundedButton
              title="Remove post"
              type="remove"
              onclick={onRemovePost}
            />
          </div>
        </div>
      )}
      <h2>{post.title}</h2>
      <div dangerouslySetInnerHTML={{ __html: content }} />
      <ErrorPanel message={error} />
    </section>
  );
}

export default PostPreview;
