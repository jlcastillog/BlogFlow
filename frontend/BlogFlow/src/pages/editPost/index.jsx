import { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ReactQuill from "react-quill";
import { createPost } from "../../services/post/postService";
import Loading from "../../components/loading";
import ErrorPanel from "../../components/error";
import "./style.css";
import "react-quill/dist/quill.snow.css";

function EditPostPage() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const location = useLocation();
  const [blog, setBlog] = useState(location.state.blog);
  const [post, setPost] = useState({ title: "", htmlContent: "", BlogId: blog.id });

  const handleSave = async (event) => {
    try {
      event.preventDefault();
      setLoading(true);
      await createPost(post);
      setLoading(false);
      navigate(`/blog/${blog.id}`, { state: { blog } });
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <section className="editPost-section">
      {loading && <Loading text="Creating post..." />}
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
        </>
      )}
    </section>
  );
}

export default EditPostPage;
