import axios from "axios";
import {
  URL_API_AUTH_BASE,
  API_POST_INSERT,
  API_POST_UPDATE,
} from "../../utils/constants";
import { refreshToken } from "../../utils/authenticationHelpers";

export const apiClient = axios.create({
  baseURL: URL_API_AUTH_BASE,
  withCredentials: true,
});

apiClient.interceptors.response.use(
  response => response,
  async (error) => refreshToken(error, apiClient)
);

export async function createUser(user) {
  const URL_API = URL_API_AUTH_BASE + API_POST_INSERT;

  try {
    await apiClient.post(URL_API, user);
  } catch (error) {
    console.log(error);
  }
}

export async function updateUser(user) {
  const URL_API = URL_API_AUTH_BASE + API_POST_UPDATE;

  try {
    await apiClient.post(URL_API + `/${user.userId}`, user);
  } catch (error) {
    console.log(error);
  }
}
