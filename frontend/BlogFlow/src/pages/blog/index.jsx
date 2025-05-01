import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ErrorPanel from "../../components/error";
import MessagePanel from "../../components/message";
import Loading from "../../components/loading";
import PostPreview from "../../components/post-preview";
import RoundedButton from "../../components/buttons/roundedButton";
import { getPostsByBlog } from "../../services/post/postService";
import { deleteBlog } from "../../services/blog/blogService";
import { getErrorMessage } from "../../components/error/helper";
import "./style.css";

function BlogPage() {
  const navigate = useNavigate();
  const auth = useAuth();
  const location = useLocation();
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState(null);
  const [message, setMassage] = useState(null);
  const [blog, setBlog] = useState(location.state.blog);
  const [posts, setPosts] = useState([]);

  const loggedUser = auth.user;

  const onCreatePost = () => {
    navigate(`/createPost/${blog.id}`, { state: { blog } });
  };

  useEffect(() => {
    async function fetchData() {
      try {
        await setErrorMessage(null);
        setLoading(true);
        const data = await getPostsByBlog(blog.id);
        setPosts(data);
      } catch (err) {
        const errorFromRespose = getErrorMessage(err);
        setErrorMessage(errorFromRespose);

        if (err.message === "Refresh token failed") {
          auth.resetUser();
        }
      } finally {
        setLoading(false);
      }
    }
    fetchData();
  }, []);

  const onRemovedPost = (post) => {
    setPosts(posts.filter((p) => p.id !== post.id));
    setMassage("Post removed successfully");
  };

  const onRemoveBlog = async () => {
    event.stopPropagation();
    try {
      event.preventDefault();
      await deleteBlog(blog.id);
      navigate(`/`);
    } catch (err) {
      setErrorMessage(err.message);
      if (err.message === "Refresh token failed") {
        auth.resetUser();
      }
    }
  };

  const onEditBlog = () => {
    console.log(blog);
    navigate(`/editBlog/${blog.id}`, { state: { blog } });
  }

  return (
    <section className="blog-section-container">
      <div className="blog-header-container">
        <div className="blog-header">
          <h1>{blog?.title}</h1>
        </div>
        {loggedUser && (
          <div className="blog-buttons-container">
            <div className="edit-button-blog">
              <RoundedButton
                title="Edit blog"
                type="edit"
                onclick={onEditBlog}
              />
            </div>
            <div className="remove-button-blog">
              <RoundedButton
                title="Remove blog"
                type="remove"
                onclick={onRemoveBlog}
              />
            </div>
          </div>
        )}
      </div>
      <div className="blog-info-container">
        <div className="blog-text-container">
          <p className="blog-author">By {blog?.author}</p>
          <p className="blog-category">{blog?.category}</p>
        </div>
        <div className="blog-image-container">
          <img
            src={`data:image/jpeg;base64,${blog?.image}`}
            alt="Imagen del blog"
            className="blog-image"
          />
        </div>
      </div>
      <div className="posts-info-container">
        <div className="posts-section-container">
          {loggedUser && (
            <div className="add-button-blog">
              <RoundedButton
                title="Create new post"
                type="add"
                onclick={onCreatePost}
              />
            </div>
          )}
          {loading && (
            <div className="loading-container">
              <Loading text="Loading posts..." />
            </div>
          )}
          {!loading && (
            <div className="posts-content">
              {posts?.map((post) => (
                <PostPreview
                  key={post.id}
                  post={post}
                  onRemovedPost={onRemovedPost}
                />
              ))}
            </div>
          )}
        </div>
      </div>
      <ErrorPanel message={errorMessage} />
      <MessagePanel message={message} />
    </section>
  );
}

export default BlogPage;
