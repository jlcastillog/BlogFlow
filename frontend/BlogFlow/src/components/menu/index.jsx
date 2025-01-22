import { NavLink } from "react-router-dom";
import { useAuth } from "../auth";
import "./style.css";

function Menu() {
  const auth = useAuth();
  const loggedUser = auth.user;

  return (
    <nav className="menu">
      <ul className="menu-left">
        <li className="logo">BlogFlow</li>
        <li>
          <NavLink to="/">Home</NavLink>
        </li>
      </ul>
      <ul className="menu-right">
        <li>
          <NavLink to="/signin">{loggedUser ? "Sign out" : "Sign in"}</NavLink>
         </li>
        {!loggedUser && (
          <>
            <li>
              <NavLink to="/signup">Sign up</NavLink>
            </li>
          </>
        )}
        <li>About</li>
        <li>Contact</li>
      </ul>
    </nav>
  );
}

export default Menu;
