import { useState } from "react";
import { useLocation } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ImageLoader from "../../components/imageLoader";
import { updateBlog } from "../../services/blog/blogService";
import Loading from "../../components/loading";
// import {base64ToFile} from "../../utils/imageHelpers.js";
import ErrorPanel from "../../components/error";
import MessagePanel from "../../components/message";
import "./style.css";

function EditBlogPage() {
  const location = useLocation();
  const [loading, setLoading] = useState(false);
  const [image, setImage] = useState(null);
  const [error, setError] = useState(null);
  const [message, setMassage] = useState(null);
  const [blog, setBlog] = useState(location.state.blog);
  const auth = useAuth();

console.log("blog", blog);

  const loggedUser = auth.user;

  const handleSubmit = async (event) => {

    try {
      event.preventDefault();
      
      setLoading(true);
      
      const form = event.target;
      const newBlog = {
        title: form[0].value,
        category: form[1].value,
        description: form[2].value,
        userId: loggedUser.userId,
      };

      const formData = new FormData();
      formData.append("Title", newBlog.title);
      formData.append("Category", newBlog.category);
      formData.append("Description", newBlog.description);                                       
      formData.append("UserId", newBlog.userId);
      formData.append("image", image);

      await updateBlog(formData, blog.id);

      setMassage("Blog updated successfully");
      setLoading(false);
    } catch (err) {
      setError(err.message);

      if(err.message === "Refresh token failed") {
        auth.resetUser();
      }
    }
    finally{
      setLoading(false);
    }
  };

  return (
    <section className="editBlog-section">
      {loading && (
        <Loading text="Loading blog..." />
      )}
      {!loading && (
        <>
          <h2>Create blog</h2>
          <form onSubmit={handleSubmit} className="editBlog-form">
            <div className="editBlog-towLine-fields">
              <label>Title</label>
              <input placeholder="Title" value={blog.title}/>
            </div>
            <div className="editBlog-towLine-fields">
              <label>Category</label>
              <input placeholder="Category" value={blog.category}/>
            </div>
            <div className="editBlog-towLine-fields">
              <label>Description</label>
              <input placeholder="Description" value={blog.description}/>
            </div>
            <div className="image-loader-field">
              <ImageLoader onImageSelect={setImage} image={image} imageUrl={blog.imageUrl + "?v=" + new Date().getTime()}/>
            </div>
            <div className="editBlog-button-container">
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

export default EditBlogPage;
