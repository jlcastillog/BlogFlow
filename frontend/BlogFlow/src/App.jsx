import { BrowserRouter, Routes, Route } from "react-router";
import Menu from "./components/menu";
import { AuthProvider } from "./components/auth";
import HomePage from "./pages/home";
import Signin from "./pages/signin";
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
          </Routes>
        </AuthProvider>
      </BrowserRouter>
    </>
  );
}

export default App;
