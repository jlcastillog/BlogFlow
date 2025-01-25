import { URL_API_BASE, API_POST_AUTH } from "../../utils/constants";

export const authenticate = async (userName, password) => {
  let user = null;

  const URL_API = URL_API_BASE + API_POST_AUTH;

  try {
    const response = await fetch(URL_API, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        password: password,
        userName: userName,
      }),
    });

    if (!response.ok) {
      throw new Error("Credenciales incorrectas");
    }

    const json = await response.json();
    console.log("Response:", json);

    user = json.data;

    console.log("User:", user);
  } catch (err) {
    throw err;
  } finally {
    console.log("Terminado login");
  }

  return user;
};
