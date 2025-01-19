const URL_API = "https://localhost:7257/api/v1/Users/Authenticate";

export const login = async (userName, password) => {
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
      throw new Error("Credenciales incorrectas"); // Maneja errores de respuesta
    }

    const data = await response.json(); // Obtén los datos de la respuesta

    console.log(data);

    //console.log("Token:", data.token); // Guarda el token o información relevante
    //// Aquí puedes guardar el token en localStorage, context, o un state manager como Redux
    //localStorage.setItem("token", data.token);
  } catch (err) {
    throw new Error(err);
  } finally {
    console.log("Terminado login");
  }
};
