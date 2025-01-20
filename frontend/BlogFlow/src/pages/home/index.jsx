import { useAuth } from "../../components/auth";
import "./style.css";

function HomePage() {
  const auth = useAuth();

  const loggedUser = auth.user;

  return (
    <section className="home">
      {loggedUser && <h1>Wellcome {loggedUser.userName}</h1>}
      {!loggedUser && <h1>Home Page</h1>}
    </section>
  );
}

export default HomePage;
