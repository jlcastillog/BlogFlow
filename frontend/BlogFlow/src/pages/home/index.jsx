import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../components/auth";
import BlogPreview from "../../components/blog-preview";
// import blogsSampleData from '../../assets/data/blogsSample.json';
import { getBlogs } from "../../services/blog/blogService";
import Loading from "../../components/loading";
import "./style.css";

function HomePage() {
  const [blogs, setBlogs] = useState([]);
  const [loading, setLoading] = useState(false);
  const auth = useAuth();
  const navigate = useNavigate();

  const loggedUser = auth.user;

  useEffect(() => {
    async function fetchData() {
      setLoading(true);
      const data = await getBlogs();
      setBlogs(data);
      setLoading(false);
    }
    fetchData();
    // setBlogs(blogsSampleData);
  }, []);

  const onCreateBlog = () => {
    navigate("/editBlog");
  };

  return (
    <section className="home">
      {loading && <Loading text="Loading blogs..." />}

      {(loggedUser && !loading) && (
        <>
          <h1>Wellcome {loggedUser.userName}</h1>
          <button onClick={onCreateBlog}>Create new blog</button>
        </>
      )}
      {!loading && (
        <div className="blogs-container">
          {blogs?.length > 0 &&
            blogs?.map((blog) => <BlogPreview key={blog.id} blog={blog} />)}
        </div>
      )}
    </section>
  );
}

export default HomePage;
