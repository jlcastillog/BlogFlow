import { useState } from "react";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../components/auth";
import "./style.css";

function Signin() {
  const auth = useAuth();

  const loginError = auth.loginError;
  const loggedUser = auth.user;

  const [userName, setUserName] = useState(
    loggedUser ? loggedUser.userName : ""
  );
  const [password, setPassword] = useState(loggedUser ? "*************" : "");

  const doLogin = (e) => {
    e.preventDefault();
    auth.login(userName, password);
  };

  const doLogout = (e) => {
    e.preventDefault();
    auth.logout();
  };

  const onSummit = loggedUser ? doLogout : doLogin;

  return (
    <section className="login">
      <div className="login-container">
        <h2>{!loggedUser ? "Sign into BlogFlow" : "Sign out"}</h2>
        {!loggedUser && (
          <p className="login-header">
            Don´t have an account?&nbsp;
            <NavLink to="/signup">Sign up</NavLink>
          </p>
        )}
        <form onSubmit={onSummit} className="login-form">
          <input
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            placeholder="Username"
            disabled={loggedUser}
          ></input>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
            disabled={loggedUser}
          ></input>
          {!loggedUser && (
            <div className="login-button-container">
              <button type="submit">Sign in</button>
            </div>
          )}
          {loggedUser && (
            <div className="login-button-container">
              <button type="submit">Sign out</button>
            </div>
          )}
        </form>
      </div>
      <div className="login-error">{loginError && <p>{loginError}</p>}</div>
    </section>
  );
}

export default Signin;
