import { NavLink } from "react-router-dom";
import "./style.css"

function Menu() {
    return (
        <nav className="menu">
            <ul className="menu-left">
                <li className="logo">BlogFlow</li>
                <li><NavLink to="/">Home</NavLink></li>              
            </ul>
            <ul className="menu-right">
                <li><NavLink to="/signin">Sign in</NavLink></li>
                <li>Sign up</li>
                <li>About</li>
                <li>Contact</li>                
            </ul>
        </nav>
    );
}

export default Menu;