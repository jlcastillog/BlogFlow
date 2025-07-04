import { createUser } from "../../services/user/userService";
import { useAuth } from "../../components/auth";
import ErrorValidationPanel from "../../components/validations";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "./style.css";

function Signup() {
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

  const doSignup = async () => {
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
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <section className="signup">
      <div className="signup-container">
        <h2>Sign Up</h2>
        <form onSubmit={handleSubmit(doSignup)} className="signup-form">
          <div className="sigup-several-fields-inline-section">
            <div className="signup-firstname-section">
              <label>First name</label>
              <input placeholder="First Name" {...register("firstName")} />
              <ErrorValidationPanel message={errors.firstName?.message} />
            </div>
            <div className="signup-lastname-section">
              <label>Last name</label>
              <input placeholder="Last Name" {...register("lastName")} />
              <ErrorValidationPanel message={errors.lastName?.message} />
            </div>
          </div>
          <div className="signup-one-field-inline-section">
            <label>User name</label>
            <input placeholder="User Name" {...register("userName")} />
            <ErrorValidationPanel message={errors.userName?.message} />
          </div>
          <div className="signup-one-field-inline-section">
            <label>Email</label>
            <input placeholder="Email" {...register("email")} />
            <ErrorValidationPanel message={errors.email?.message} />
          </div>
          <div className="signup-one-field-inline-section">
            <label>Password</label>
            <input
              placeholder="Password"
              type="password"
              {...register("password")}
            />
            <ErrorValidationPanel message={errors.password?.message} />
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
