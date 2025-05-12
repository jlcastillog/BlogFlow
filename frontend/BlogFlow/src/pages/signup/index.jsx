import { createUser } from "../../services/user/userService";
import { useAuth } from "../../components/auth";
import "./style.css";

function Signup() {
  const auth = useAuth();

  const doSignup = async (event) => {
    event.preventDefault();
    const form = event.target;
    const user = {
      firstName: form[0].value,
      lastName: form[1].value,
      userName: form[2].value,
      email: form[3].value,
      password: form[4].value,
      token: "",
    };

    try {
      await createUser(user);
      auth.login(user.userName, user.password);
    }
    catch (error) {
      console.error(error);
    }
  };

  return (
    <section className="signup">
      <div className="signup-container">
        <h2>Sign Up</h2>
        <form onSubmit={doSignup} className="signup-form">
          <div className="sigup-several-fields-inline-section">
            <div className="signup-firstname-section">
              <label>First name</label>
              <input placeholder="First Name" />
            </div>
            <div className="signup-lastname-section">
              <label>Last name</label>
              <input placeholder="Last Name" />
            </div>
          </div>
          <div className="signup-one-field-inline-section">
            <label>User name</label>
            <input placeholder="User Name" />
          </div>
          <div className="signup-one-field-inline-section">
            <label>Email</label>
            <input placeholder="Email" />
          </div>
          <div className="signup-one-field-inline-section">
            <label>Password</label>
            <input placeholder="Password" type="password" />
          </div>
          <div className="signup-button-container">
            <button type="submit">Create Account</button>
          </div>
        </form>
      </div>
    </section>
  );
}

export default Signup;
