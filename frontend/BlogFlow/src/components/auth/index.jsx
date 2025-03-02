/* eslint-disable react/prop-types */
import React from "react";
import { useNavigate, Navigate } from "react-router-dom";
import {
  authenticate,
  logoutService,
} from "../../services/authenticate/authenticateService.js";
import {
  setLocalStorageUser,
  getLocalStorageUser,
  removeLocalStorageUser,
  updateLocalStorageUser,
} from "../../utils/localStorage.js";

const AuthContext = React.createContext();

function AuthProvider({ children }) {
  const navigate = useNavigate();
  const [user, setUser] = React.useState(getLocalStorageUser());

  const login = async (username, password) => {
    const loggedUser = await authenticate(username, password);

    if (loggedUser) {
      setLocalStorageUser(loggedUser);
      setUser(loggedUser);
      navigate("/");
    } else {
      throw new Error("Invalid username or password. Please try again.");
    }
  };

  const logout = async () => {
    if (user) {
      await logoutService();
      setUser(null);
      removeLocalStorageUser();
      navigate("/");
    } else {
      throw new Error("Not logged in.");
    }
  };

  const updateUser = (user) => {
    updateLocalStorageUser(user);
    setUser(user);
  };

  const resetUser = () => {
    updateLocalStorageUser(null);
    setUser(null);
    navigate("/signin");
  };

  const auth = { user, login, logout, updateUser, resetUser };

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
