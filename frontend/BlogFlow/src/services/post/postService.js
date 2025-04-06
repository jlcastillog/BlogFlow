import axios from "axios";
import {
  URL_API_CORE_BASE,
  API__POST_POST_INSERT,
  API__GET_POST_GETBYBLOG,
  API__POST_POST_UPDATE,
  API__POST_POST_DELETE,
} from "../../utils/constants";
import { refreshToken } from "../../utils/authenticationHelpers";

export const apiClient = axios.create({
  baseURL: URL_API_CORE_BASE,
  withCredentials: true,
});

apiClient.interceptors.response.use(
  response => response,
  async (error) => refreshToken(error, apiClient)
);

export async function createPost(data) {
  const URL_API = URL_API_CORE_BASE + API__POST_POST_INSERT;

  await apiClient.post(URL_API, data);
}

export async function getPostsByBlog(blogId) {
  const URL_API = URL_API_CORE_BASE + API__GET_POST_GETBYBLOG;

  const response = await apiClient.get(URL_API+ `/${blogId}`);
  return response.data.data;
}

export async function updatePost(data, postId) {
  const URL_API = URL_API_CORE_BASE + API__POST_POST_UPDATE;
  await apiClient.post(URL_API + `/${postId}`, data);
}

export async function deletePost(postId) {
  const URL_API = URL_API_CORE_BASE + API__POST_POST_DELETE;

  await apiClient.delete(URL_API + `/${postId}`);
}
