import React from "react";
import { useAuth } from "../../components/auth";
import { updateUser } from "../../services/user/userService";
import "./style.css";

function Profile() {
  const auth = useAuth();

  const [loggedUser, setLoggedUser] = React.useState(auth.user);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setLoggedUser({ ...loggedUser, [name]: value });
  };

  const doSave = (event) => {
    event.preventDefault();

    try {
      updateUser(loggedUser);
      auth.updateUser(loggedUser);
    } catch (err) {
      if(err.message === "Refresh token failed") {
        auth.resetUser();
      }
    }
  };

  return (
    <section className="profile-section">
      <div className="profile-container">
        <h2>Profile</h2>
        <form onSubmit={doSave} className="profile-form">
          <div className="profile-several-fields-inline-section">
            <div className="profile-firstname-section">
              <label>First name</label>
              <input
                name="firstName"
                placeholder="First Name"
                value={loggedUser.firstName}
                onChange={handleInputChange}
              />
            </div>
            <div className="profile-lastname-section">
              <label>Last name</label>
              <input
                name="lastName"
                placeholder="Last Name"
                value={loggedUser.lastName}
                onChange={handleInputChange}
              />
            </div>
          </div>
          <div className="profile-one-field-inline-section">
            <label>User name</label>
            <input
              name="userName"
              placeholder="User Name"
              value={loggedUser.userName}
              onChange={handleInputChange}
            />
          </div>
          <div className="profile-one-field-inline-section">
            <label>Email</label>
            <input
              name="email"
              placeholder="Email"
              value={loggedUser.email}
              onChange={handleInputChange}
            />
          </div>
          <div className="profile-button-container">
            <button type="submit">Save changes</button>
          </div>
        </form>
      </div>
    </section>
  );
}

export default Profile;
