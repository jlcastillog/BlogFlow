import { useState } from "react";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../components/auth";
import "./style.css";

function Signin() {
  const auth = useAuth();
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");

  const doLogin = (e) => {
    e.preventDefault();
    console.log("Login: ", userName, password);
    auth.authenticate(userName, password);
  };

  return (
    <section className="login">
      <div className="login-container">
        <h2>Sign into BlogFlow</h2>
        <p className="login-header">
          Don´t have an account?&nbsp;
          <NavLink to="/signup">Sign up</NavLink>
        </p>
        <form onSubmit={doLogin} className="login-form">
          <input
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            placeholder="Username"
          ></input>
          <input
          type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
          ></input>
          <div className="login-button-container">
            <button type="submit">SIGN IN</button>
          </div>
        </form>
      </div>
    </section>
  );
}

export default Signin;
