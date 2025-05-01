export function base64ToFile(base64, fileName, mimeType) {
    const byteCharacters = atob(base64); // Decodifica base64 a binario
    const byteNumbers = Array.from(byteCharacters, char => char.charCodeAt(0));
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], { type: mimeType });
  
    return new File([blob], fileName, {
      type: mimeType,
      lastModified: Date.now()
    });
  }