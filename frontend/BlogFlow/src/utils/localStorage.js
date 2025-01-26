export function setLocalStorageUser(user) {
  const expirationInMinutes = 30; // Tiempo de expiración de la sesión en minutos
  const expirationTime = new Date().getTime() + expirationInMinutes * 60 * 1000; // Tiempo en milisegundos
  const sessionData = {
    user: user,
    expiration: expirationTime,
  };
  localStorage.setItem("loggedUser", JSON.stringify(sessionData));
}

export function getLocalStorageUser() {
  const sessionData = JSON.parse(localStorage.getItem("loggedUser"));
  if (!sessionData) return null;
  const currentTime = new Date().getTime();

  // Verificar si la sesión ha expirado
  if (currentTime > sessionData.expiration) {
    removeLocalStorageUser(); // Eliminar sesión caducada
    return null;
  }

  return sessionData.user;
}

export function updateLocalStorageUser(user) {
  const sessionData = JSON.parse(localStorage.getItem("loggedUser"));
  sessionData.user = user;
  setLocalStorageUser(sessionData);
}

export function removeLocalStorageUser() {
  localStorage.removeItem("loggedUser");
}
