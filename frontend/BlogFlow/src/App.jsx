import { BrowserRouter, Routes, Route } from "react-router";
import Menu from "./components/menu";
import { AuthProvider } from "./components/auth";
import HomePage from "./pages/home";
import Signin from "./pages/signin";
import Signup from "./pages/signup";
import Profile from "./pages/profile";
import BlogPage from "./pages/blog";
import EditBlogPage from "./pages/editBlog";
import PostPage from "./pages/post";
import CreatePostPage from "./pages/createPost";
import EditPostPage from "./pages/editPost";
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
            <Route path="/post/:id" element={<PostPage/>} />
            <Route path="/createPost/:idBlog" element={<CreatePostPage/>} />
            <Route path="/editPost/:idPost" element={<EditPostPage/>} />
          </Routes>
        </AuthProvider>
      </BrowserRouter>
    </>
  );
}

export default App;
