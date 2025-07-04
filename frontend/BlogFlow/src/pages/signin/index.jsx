import { useState } from "react";
import { NavLink } from "react-router-dom";
import { useAuth } from "../../components/auth";
import ErrorPanel from "../../components/error";
import { getErrorMessage } from "../../components/error/helper";
import { useForm } from "react-hook-form";
import ErrorValidationPanel from "../../components/validations";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "./style.css";

function Signin() {
  const schema = yup.object().shape({
    userName: yup.string().required("User name field is mandatory"),
    password: yup.string().required("Password field is mandatory"),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
  });

  const auth = useAuth();
  const [errorMessage, setErrorMessage] = useState(null);

  const loggedUser = auth.user;

  const [userName, setUserName] = useState(
    loggedUser ? loggedUser.userName : ""
  );

  const [password, setPassword] = useState(loggedUser ? "*************" : "");

  const doLogin = async () => {
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

  const doLogout = async () => {
    try {
      await setErrorMessage(null);
      await auth.logout();
    } catch (err) {
      if (err.message === "Refresh token failed") {
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
        <form onSubmit={handleSubmit(onSummit)} className="login-form">
          <input
            {...register("userName")}
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            placeholder="Username"
            disabled={loggedUser}
          ></input>
          <ErrorValidationPanel message={errors.userName?.message} />
          <input
            {...register("password")}
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Password"
            disabled={loggedUser}
          ></input>
          <ErrorValidationPanel message={errors.password?.message} />
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
