/* eslint-disable react/prop-types */
import React from "react";
import { useNavigate, Navigate } from "react-router-dom";

// Simulate a backend user list
const adminList = [
    { username: "admin"},
    { username: "jlcastillog"},
    { username: "testuser"},
    { username: "guestuser"},
    { username: "editoruser"},
  ];

  const AuthContext = React.createContext();

function AuthProvider({ children }) {
    const navigate = useNavigate();
    const [user, setUser] = React.useState(null);
  
    const login = (username) => {
      const user = adminList.find((user) => user.username === username);
      if (user === undefined) {
        navigate("/error-login");
      } else {
        setUser(user);
        navigate("/profile");
      }
    };
  
    const logout = () => {
      setUser(null);
      navigate("/");
    };
  
    const auth = { user, login, logout };
  
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