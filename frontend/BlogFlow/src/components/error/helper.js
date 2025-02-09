export function getErrorMessage(error) {
  if (error.response) {
    return error.response.data.message;
  } else if (error.request) {
    return "Server is not responding";
  } else if (error.message){
    return error.message;
  } else{
    return "An error occurred";
  } 
}