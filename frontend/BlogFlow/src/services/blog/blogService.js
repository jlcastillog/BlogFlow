import axios from "axios";
import {
  URL_API_CORE_BASE,
  API__BLOG_POST_INSERT,
  API__BLOG_POST_GETALL,
  API__BLOG_POST_DELETE,
} from "../../utils/constants";
import { refreshToken } from "../../utils/authenticationHelpers";

export const apiClient = axios.create({
  baseURL: URL_API_CORE_BASE,
  withCredentials: true,
});

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => refreshToken(error, apiClient)
);

export async function createBlog(data) {
  const URL_API = URL_API_CORE_BASE + API__BLOG_POST_INSERT;

  await apiClient.post(URL_API, data, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
}

export async function getBlogs() {
  const URL_API = URL_API_CORE_BASE + API__BLOG_POST_GETALL;

  const response = await apiClient.get(URL_API);
  return response.data.data;
}

export async function deleteBlog(blogId) {
  const URL_API = URL_API_CORE_BASE + API__BLOG_POST_DELETE;

  await apiClient.delete(URL_API + `/${blogId}`);
}
