import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ImageLoader from "../../components/imageLoader";
import { createBlog } from "../../services/blog/blogService";
import Loading from "../../components/loading";
import "./style.css";

function CreateBlogPage() {
  const [loading, setLoading] = useState(false);
  const [image, setImage] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const auth = useAuth();

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

      await createBlog(formData);

      navigate("/");

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
    <section className="createBlog-section">
      {loading && (
        <Loading text="Creating blog..." />
      )}
      {!loading && (
        <>
          <h2>Create blog</h2>
          <form onSubmit={handleSubmit} className="createBlog-form">
            <div className="createBlog-towLine-fields">
              <label>Title</label>
              <input placeholder="Title" />
            </div>
            <div className="createBlog-towLine-fields">
              <label>Category</label>
              <input placeholder="Category" />
            </div>
            <div className="createBlog-towLine-fields">
              <label>Description</label>
              <input placeholder="Description" />
            </div>
            <div className="image-loader-field">
              <ImageLoader onImageSelect={setImage} />
            </div>
            <div className="createBlog-button-container">
              <button type="submit">Create Blog</button>
            </div>
          </form>
          {
            error && (
              <div className="error-message">
                <p>{error}</p>
              </div>
            )
          }
        </>
      )}
    </section>
  );
}

export default CreateBlogPage;
