/* eslint-disable react/prop-types */
import React from "react";
import { useNavigate, Navigate } from "react-router-dom";
import { authenticate } from "../../services/authenticate/authenticateService.js";
import { setLocalStorageUser, getLocalStorageUser } from "../../utils/localStorage.js";

const AuthContext = React.createContext();

function AuthProvider({ children }) {
  const navigate = useNavigate();
  const [user, setUser] = React.useState(getLocalStorageUser());
  const [loginError, setLoginError] = React.useState(null);

  const login = async (username, password) => {
    try {
      const loggedUser = await authenticate(username, password);

      if (loggedUser) {
        setLocalStorageUser(loggedUser);
        setUser(loggedUser);
        setLoginError(null);
        navigate("/");
      } else {
        setLoginError("Invalid username or password. Please try again.");
      }
    } catch (err) {
      console.log("Login error", err);
      setLoginError("Invalid username or password. Please try again.");
    }
  };

  const logout = () => {
    setUser(null);
    navigate("/");
  };

  const auth = { user, loginError, login, logout };

  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
}

function useAuth() {
  const context = React.useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}

function AuthRoute(props) {
  const auth = useAuth();

  if (!auth.user) {
    return <Navigate to="/login" />;
  }

  return props.children;
}

export { AuthProvider, useAuth, AuthRoute };
