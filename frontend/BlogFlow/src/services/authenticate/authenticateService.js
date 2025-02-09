import {
  URL_API_AUTH_BASE,
  API_POST_AUTH,
  API_POST_LOGOUT,
} from "../../utils/constants";
import { apiClient } from "../user/userService";

export const authenticate = async (userName, password) => {
  let user = null;

  const URL_API = URL_API_AUTH_BASE + API_POST_AUTH;

  const response = await apiClient.post(URL_API, {
    userName: userName,
    password: password,
  });

  user = await response.data.data;

  return user;
};

export const logoutService = async () => {
  const URL_API = URL_API_AUTH_BASE + API_POST_LOGOUT;
  await apiClient.post(URL_API);
};
