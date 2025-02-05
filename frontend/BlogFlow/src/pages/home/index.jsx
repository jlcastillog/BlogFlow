import { useAuth } from "../../components/auth";
import BlogPreview from "../../components/blog-preview";
import blogsSampleData from '../../assets/data/blogsSample.json';
import "./style.css";

function HomePage() {
  const auth = useAuth();

  const loggedUser = auth.user;

  return (
    <section className="home">
      {loggedUser && <h1>Wellcome {loggedUser.userName }</h1>}
      {!loggedUser && <h1>Home Page</h1>}
      <div className="blogs-container">
        {blogsSampleData?.length > 0 &&
          blogsSampleData?.map((blog) => (
            <BlogPreview key={blog.id} blog={blog} />
          ))}
      </div>
    </section>
  );
}

export default HomePage;
