import axios from "axios";
import {
  URL_API_CORE_BASE,
  API__POST_POST_INSERT,
  API__GET_POST_GETBYBLOG,
} from "../../utils/constants";

export const apiClient = axios.create({
  baseURL: URL_API_CORE_BASE,
  withCredentials: true,
});

export async function createPost(data) {
  const URL_API = URL_API_CORE_BASE + API__POST_POST_INSERT;

  await apiClient.post(URL_API, data);
}

export async function getPostsByBlog(blogId) {
  const URL_API = URL_API_CORE_BASE + API__GET_POST_GETBYBLOG + blogId;

  const response = await apiClient.get(URL_API);
  return response.data.data;
}
