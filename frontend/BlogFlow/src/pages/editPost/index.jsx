import { useState } from "react";
import ReactQuill from "react-quill";
import "./style.css";
import "react-quill/dist/quill.snow.css";

function EditPostPage() {
  const [post, setPost] = useState({ title: "", content: "" });
  return (
    <section className="editPost-section">
      <h2>Edit Post</h2>
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
          value={post.content}
          onChange={(value) => setPost({ ...post, content: value })}
          theme="snow"
          className="editPost-quill"
        />
      </div>
      {/* <button onClick={handleSave}>Guardar</button> */}
    </section>
  );
}

export default EditPostPage;
