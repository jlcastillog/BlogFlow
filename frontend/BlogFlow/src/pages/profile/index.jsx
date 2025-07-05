import React from "react";
import { useAuth } from "../../components/auth";
import { updateUser } from "../../services/user/userService";
import { useForm } from "react-hook-form";
import ErrorValidationPanel from "../../components/validations";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "./style.css";

function Profile() {
  const schema = yup.object().shape({
    firstName: yup.string().required("First name field is mandatory"),
    lastName: yup.string().required("Last name field is mandatory"),
    email: yup.string().email().required("Email field is mandatory"),
    userName: yup.string().required("User name field is mandatory"),
    password: yup.string().required("Password field is mandatory"),
  });

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    resolver: yupResolver(schema),
  });

  const auth = useAuth();

  const [loggedUser, setLoggedUser] = React.useState(auth.user);

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setLoggedUser({ ...loggedUser, [name]: value });
  };

  const doSave = () => {
    try {
      updateUser(loggedUser);
      auth.updateUser(loggedUser);
    } catch (err) {
      if (err.message === "Refresh token failed") {
        auth.resetUser();
      }
    }
  };

  return (
    <section className="profile-section">
      <div className="profile-container">
        <h2>Profile</h2>
        <form onSubmit={handleSubmit(doSave)} className="profile-form">
          <div className="profile-several-fields-inline-section">
            <div className="profile-firstname-section">
              <label>First name</label>
              <input
                {...register("firstName")}
                name="firstName"
                placeholder="First Name"
                value={loggedUser.firstName}
                onChange={handleInputChange}
              />
              <ErrorValidationPanel message={errors.firstName?.message} />
            </div>
            <div className="profile-lastname-section">
              <label>Last name</label>
              <input
                {...register("lastName")}
                name="lastName"
                placeholder="Last Name"
                value={loggedUser.lastName}
                onChange={handleInputChange}
              />
              <ErrorValidationPanel message={errors.lastName?.message} />
            </div>
          </div>
          <div className="profile-one-field-inline-section">
            <label>User name</label>
            <input
              {...register("userName")}
              name="userName"
              placeholder="User Name"
              value={loggedUser.userName}
              onChange={handleInputChange}
            />
            <ErrorValidationPanel message={errors.userName?.message} />
          </div>
          <div className="profile-one-field-inline-section">
            <label>Email</label>
            <input
              {...register("email")}
              name="email"
              placeholder="Email"
              value={loggedUser.email}
              onChange={handleInputChange}
            />
            <ErrorValidationPanel message={errors.email?.message} />
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
