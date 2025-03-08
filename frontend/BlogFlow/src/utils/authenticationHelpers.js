import axios from "axios";
import { URL_API_AUTH_BASE, API_POST_REFRESH_TOKEN } from "./constants";

export const apiClient = axios.create({
  baseURL: URL_API_AUTH_BASE,
  withCredentials: true,
});

export async function refreshToken(error, apiBusiness) {
  if (error?.response?.status === 401) {
    // Refresh token
    try {
      const url = URL_API_AUTH_BASE + API_POST_REFRESH_TOKEN;
      const reponse = await apiClient.post(url);

      if (reponse.status === 200) {
        return apiBusiness(error.config);
      }
    } catch (refreshError) {
      console.error("Could not refresh token", refreshError);
      throw new Error("Refresh token failed");
    }
  }
  return Promise.reject(error);
}
