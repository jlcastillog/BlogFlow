import axios from "axios";
import { URL_API_CORE_BASE } from "../../utils/constants";
import { refreshToken } from "../../utils/authenticationHelpers";

export const apiClient = axios.create({
  baseURL: URL_API_CORE_BASE,
  withCredentials: true,
});

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => refreshToken(error, apiClient)
);

export async function followBlog(blogId, userId) {
  const URL_API = `${URL_API_CORE_BASE}Blogs/${blogId}/followers`;
  console.log("Follow blog URL:", URL_API);
  await apiClient.post(URL_API, { userId });
}

export async function unfollowBlog(blogId, userId) {
  const URL_API = `${URL_API_CORE_BASE}Blogs/${blogId}/followers/${userId}`;
  await apiClient.delete(URL_API);
}

export async function getFollowers(blogId) {
  const URL_API = `${URL_API_CORE_BASE}Blogs/${blogId}/followers`;
  const response = await apiClient.get(URL_API);
  console.log("Get followers response:", response.data);
  return response.data;
}