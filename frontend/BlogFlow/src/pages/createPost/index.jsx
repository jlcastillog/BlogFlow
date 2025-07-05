import { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import ReactQuill from "react-quill";
import { createPost } from "../../services/post/postService";
import Loading from "../../components/loading";
import ErrorPanel from "../../components/error";
import { useAuth } from "../../components/auth";
import { useForm, Controller } from "react-hook-form";
import ErrorValidationPanel from "../../components/validations";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "./style.css";
import "react-quill/dist/quill.snow.css";

function CreatePostPage() {
  const schema = yup.object().shape({
    title: yup.string().required("Title field is mandatory"),
    content: yup
      .string()
      .required("Content field is mandatory")
      .min(10, "Minimun 10 characters"),
  });

  const {
    register,
    control,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
  });

  const auth = useAuth();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const location = useLocation();
  const [blog, setBlog] = useState(location.state.blog);
  const [post, setPost] = useState({
    title: "",
    htmlContent: "",
    BlogId: blog.id,
  });

  const createPost = async () => {
    try {
      setLoading(true);
      await createPost(post);
      setLoading(false);
      navigate(`/blog/${blog.id}`, { state: { blog } });
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
    <section className="createPost-section">
      {loading && <Loading text="Creating post..." />}
      {!loading && (
        <>
          <h2>Edit Post</h2>
          <form onSubmit={handleSubmit(createPost)} className="createPost-form">
            <div className="createPost-title">
              <label>Title</label>
              <input
                {...register("title")}
                type="text"
                value={post.title}
                onChange={(e) => setPost({ ...post, title: e.target.value })}
              />
              <ErrorValidationPanel message={errors.title?.message} />
            </div>
            <div className="createPost-content">
              <Controller
                name="content"
                control={control}
                render={({ field }) => (
                  <ReactQuill
                    {...field}
                    value={post.htmlContent}
                    onChange={(value) => {
                      setPost({ ...post, htmlContent: value });
                      field.onChange(value);
                    }}
                    theme="snow"
                    className="createPost-quill"
                  />
                )}
              />
              <ErrorValidationPanel message={errors.content?.message} />
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

export default CreatePostPage;
