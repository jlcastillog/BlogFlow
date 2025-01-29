import axios from "axios";
import {
  URL_API_BASE,
  API_POST_INSERT,
  API_POST_UPDATE,
} from "../../utils/constants";

export const apiClient = axios.create({
  baseURL: URL_API_BASE,
});

export async function createUser(user) {
  const URL_API = URL_API_BASE + API_POST_INSERT;

  try {
    await apiClient.post(URL_API, user);
  } catch (error) {
    console.log(error);
  }
}

export async function updateUser(user) {
  const URL_API = URL_API_BASE + API_POST_UPDATE;

  try {
    await apiClient.post(URL_API + `/${user.userId}`, user);
  } catch (error) {
    console.log(error);
  }
}
