const URL_API = "https://localhost:7257/api/v1/Users/Insert";

export function createUser(user) {
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