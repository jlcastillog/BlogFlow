export function setLocalStorageUser(user) {
    localStorage.setItem("loggedUser", JSON.stringify(user));
}

export function getLocalStorageUser() {
    return JSON.parse(localStorage.getItem("loggedUser"));
}