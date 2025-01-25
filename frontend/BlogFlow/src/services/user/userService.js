import { URL_API_BASE, API_POST_INSERT } from "../../utils/constants";

export function createUser(user) {
  const URL_API = URL_API_BASE + API_POST_INSERT;

  return fetch(URL_API, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(user),
  }).then((response) => {
    if (!response.ok) {
      throw new Error("Error al crear el usuario");
    }

    return response.json();
  });
}
