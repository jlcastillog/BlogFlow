import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ErrorPanel from "../../components/error";
import Loading from "../../components/loading";
import PostPreview from "../../components/post-preview";
import RoundedButton from "../../components/buttons/roundedButton";
import { getPostsByBlog } from "../../services/post/postService";
import { getErrorMessage } from "../../components/error/helper";
import "./style.css";

function BlogPage() {
  const navigate = useNavigate();
  const auth = useAuth();
  const location = useLocation();
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState(null);
  const [blog, setBlog] = useState(location.state.blog);
  const [posts, setPosts] = useState([]);
  
  const loggedUser = auth.user;

  const onCreateBlog = () => {
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
        
        if(err.message === "Refresh token failed") {
          auth.resetUser();
        }
      } finally {
        setLoading(false);
      }
    }
    fetchData();
  }, []);

  return (
    <section className="blog-section-container">
      <h1>{blog?.title}</h1>
      <div className="blog-info">
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
        <div className="posts-section-container">
          {loggedUser && (<div className="add-button-blog">
            <RoundedButton
              title="Create new post"
              type="add"
              onclick={onCreateBlog}
            />
          </div>)}
            {loading && (
              <div className="loading-container">
                <Loading text="Loading posts..." />
              </div>
            )}
            {!loading && (
              <div className="posts-content">
                {posts?.map((post) => (
                  <PostPreview key={post.id} post={post} />
                ))}
              </div>
            )}
        </div>
      </div>
      <ErrorPanel message={errorMessage} />
    </section>
  );
}

export default BlogPage;
