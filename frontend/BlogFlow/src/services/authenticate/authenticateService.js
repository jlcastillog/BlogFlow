import { URL_API_BASE, API_POST_AUTH } from "../../utils/constants";
import { apiClient } from "../user/userService";

export const authenticate = async (userName, password) => {
  let user = null;

  const URL_API = URL_API_BASE + API_POST_AUTH;

  try {
    const response = await apiClient.post(URL_API, {
      userName: userName,
      password: password,
    });

    user = await response.data.data;

    console.log("User:", user);
  } catch (err) {
    throw err;
  } finally {
    console.log("Terminado login");
  }

  return user;
}