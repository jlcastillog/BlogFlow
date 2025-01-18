import { NavLink } from "react-router-dom";
import "./style.css";

function Signin() {
  return (
    <section className="login">
      <div className="login-container">
        <h2>Sign into BlogFlow</h2>
        <p className="login-header">
          Don´t have an account?&nbsp;
          <NavLink to="/signup">Sign up</NavLink>
        </p>
        <form className="login-form">
          <input placeholder="Username"></input>
          <input placeholder="Password"></input>
          <div className="login-button-container">
            <button>SIGN IN</button>
          </div>
        </form>
      </div>
    </section>
  );
}

export default Signin;
