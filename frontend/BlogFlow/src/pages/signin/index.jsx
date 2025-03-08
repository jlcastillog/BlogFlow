import { useState } from "react";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ErrorPanel from "../../components/error";
import { getErrorMessage } from "../../components/error/helper";
import "./style.css";

function Signin() {
  const auth = useAuth();
  const [errorMessage, setErrorMessage] = useState(null);

  const loggedUser = auth.user;

  const [userName, setUserName] = useState(
    loggedUser ? loggedUser.userName : ""
  );

  const [password, setPassword] = useState(loggedUser ? "*************" : "");

  const doLogin = async (e) => {
    e.preventDefault();
    try {
      await setErrorMessage(null);
      if (!userName || !password) {
        throw new Error("Please enter a username and password.");
      }
      await auth.login(userName, password);
    } catch (err) {
      const errorFromRespose = getErrorMessage(err);
      setErrorMessage(errorFromRespose);
    }
  };

  const doLogout = async (e) => {
    e.preventDefault();

    try {
      await setErrorMessage(null);
      await auth.logout();
    } catch (err) {
      if(err.message === "Refresh token failed") {
        auth.resetUser();
      }
      const errorFromRespose = getErrorMessage(err);
      setErrorMessage(errorFromRespose);
    }
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
      <ErrorPanel message={errorMessage} />
    </section>
  );
}

export default Signin;
