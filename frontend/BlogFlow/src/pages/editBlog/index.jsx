import { useState } from "react";
import { useLocation } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ImageLoader from "../../components/imageLoader";
import { updateBlog } from "../../services/blog/blogService";
import Loading from "../../components/loading";
import ErrorPanel from "../../components/error";
import MessagePanel from "../../components/message";
import { useForm } from "react-hook-form";
import ErrorValidationPanel from "../../components/validations";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "./style.css";

function EditBlogPage() {
  const schema = yup.object().shape({
    title: yup.string().required("Title field is mandatory"),
    category: yup.string().required("Category field is mandatory")
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
  });

  const location = useLocation();
  const [loading, setLoading] = useState(false);
  const [image, setImage] = useState(null);
  const [error, setError] = useState(null);
  const [message, setMassage] = useState(null);
  const [blog, setBlog] = useState(location.state.blog);
  const auth = useAuth();

  const loggedUser = auth.user;

  const saveBlog = async (event) => {
    try {
      setLoading(true);
      setError(null);

      const form = event.target;
      const newBlog = {
        title: form[0].value,
        category: form[1].value,
        description: form[2].value,
        userId: loggedUser.userId,
      };

      const formData = new FormData();
      formData.append("Id", blog.id);
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

      if (err.message === "Refresh token failed") {
        auth.resetUser();
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <section className="editBlog-section">
      {loading && <Loading text="Loading blog..." />}
      {!loading && (
        <>
          <h2>Create blog</h2>
          <form onSubmit={handleSubmit(saveBlog)} className="editBlog-form">
            <div className="editBlog-towLine-fields">
              <label>Title</label>
              <input
                {...register("title")}
                placeholder="Title"
                value={blog.title}
                onChange={(e) => setBlog({ ...blog, title: e.target.value })}
              />
              <ErrorValidationPanel message={errors.title?.message} />
            </div>
            <div className="editBlog-towLine-fields">
              <label>Category</label>
              <input
                {...register("category")}
                placeholder="Category"
                value={blog.category}
                onChange={(e) => setBlog({ ...blog, category: e.target.value })}
              />
              <ErrorValidationPanel message={errors.category?.message} />
            </div>
            <div className="editBlog-towLine-fields">
              <label>Description</label>
              <input
                {...register("description")}
                placeholder="Description"
                value={blog.description}
                onChange={(e) =>
                  setBlog({ ...blog, description: e.target.value })
                }
              />
            </div>
            <div className="image-loader-field">
              <ImageLoader
                onImageSelect={setImage}
                image={image}
                imageUrl={blog.imageUrl + "?v=" + new Date().getTime()}
              />
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
