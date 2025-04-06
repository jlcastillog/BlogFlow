import { useState } from "react";
import { useLocation } from "react-router-dom";
import ReactQuill from "react-quill";
import { updatePost } from "../../services/post/postService";
import Loading from "../../components/loading";
import ErrorPanel from "../../components/error";
import MessagePanel from "../../components/message";
import { useAuth } from "../../components/auth";
import "./style.css";
import "react-quill/dist/quill.snow.css";

function EditPostPage() {
  const auth = useAuth();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [message, setMassage] = useState(null);
  const location = useLocation();
  const [post, setPost] = useState(location.state.post);

  const handleSave = async (event) => {
    try {
      event.preventDefault();
      setLoading(true);
      await updatePost(post, post.id);
      setMassage("Post updated successfully");
      setLoading(false);
    } catch (err) {
      setError(err.message);
      if(err.message === "Refresh token failed") {
        auth.resetUser();
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <section className="editPost-section">
      {loading && <Loading text="Updating post..." />}
      {!loading && (
        <>
          <h2>Edit Post</h2>
          <form onSubmit={handleSave} className="editPost-form">
          <div className="editPost-title">
            <label>Title</label>
            <input
              type="text"
              value={post.title}
              onChange={(e) => setPost({ ...post, title: e.target.value })}
            />
          </div>
          <div className="editPost-content">
            <ReactQuill
              value={post.htmlContent}
              onChange={(value) => setPost({ ...post, htmlContent: value })}
              theme="snow"
              className="editPost-quill"
            />
          </div>
          <div>
            <button type="submit">Save</button>
          </div>
          </form>
          <ErrorPanel message={error} />
          <MessagePanel message={message} />
        </>
      )}
    </section>
  );
}

export default EditPostPage;
