import "./style.css";

function Signup() {
  const doSignup = () => {};

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
            <label>Work email</label>
            <input placeholder="Work email" />
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
