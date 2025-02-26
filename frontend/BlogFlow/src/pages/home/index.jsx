import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../components/auth";
import BlogPreview from "../../components/blog-preview";
// import blogsSampleData from '../../assets/data/blogsSample.json';
import { getBlogs } from "../../services/blog/blogService";
import ErrorPanel from "../../components/error";
import { getErrorMessage } from "../../components/error/helper";
import Loading from "../../components/loading";
import RoundedButton from "../../components/buttons/roundedButton";
import "./style.css";

function HomePage() {
  const [blogs, setBlogs] = useState([]);
  const [loading, setLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState(null);
  const auth = useAuth();
  const navigate = useNavigate();

  const loggedUser = auth.user;

  useEffect(() => {
    async function fetchData() {
      try {
        await setErrorMessage(null);
        setLoading(true);
        const data = await getBlogs();
        console.log(data);
        setBlogs(data);
      } catch (err) {
        const errorFromRespose = getErrorMessage(err);
        setErrorMessage(errorFromRespose);
      } finally {
        setLoading(false);
      }
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
      {loggedUser && !loading && (
        <>
          <h1>Wellcome {loggedUser.userName}</h1>
        </>
      )}
      {!loading && (
        <div className="blogs-section">
          {loggedUser && (
            <div className="add-button-home">
              <RoundedButton
                title="Create new blog"
                type="add"
                onclick={onCreateBlog}
              />
            </div>
          )}
          <div className="blogs-container">
            {blogs?.length > 0 &&
              blogs?.map((blog) => <BlogPreview key={blog.id} blog={blog} />)}
          </div>
        </div>
      )}
      <ErrorPanel message={errorMessage} />
    </section>
  );
}

export default HomePage;
