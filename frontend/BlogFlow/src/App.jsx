import { BrowserRouter, Routes, Route } from "react-router";
import Menu from "./components/menu";
import { AuthProvider } from "./components/auth";
import HomePage from "./pages/home";
import Signin from "./pages/signin";
import Signup from "./pages/signup";
import Profile from "./pages/profile";
import BlogPage from "./pages/blog";
import EditBlogPage from "./pages/editBlog";
import "./App.css";

function App() {
  return (
    <>
      <BrowserRouter>
        <AuthProvider>
          <Menu />
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/signin" element={<Signin />} />
            <Route path="/signup" element={<Signup/>} />
            <Route path="/profile" element={<Profile/>} />
            <Route path="/blog/:id" element={<BlogPage/>} />
            <Route path="/editBlog" element={<EditBlogPage/>} />
          </Routes>
        </AuthProvider>
      </BrowserRouter>
    </>
  );
}

export default App;
