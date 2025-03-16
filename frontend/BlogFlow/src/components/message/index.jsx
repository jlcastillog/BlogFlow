import { useState, useEffect } from "react";
import { motion } from "framer-motion";
import { X } from "lucide-react";
import "./style.css";

function MessagePanel({ message }) {
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    if (message) {
      setIsVisible(true); // Mostrar el panel cuando el mensaje es válido
    } else {
      setIsVisible(false); // Ocultar cuando no haya mensaje
    }
  }, [message]);

  if (!isVisible) return null;

  return (
    <motion.div
      initial={{ y: 50, opacity: 0 }}
      animate={{ y: 0, opacity: 1 }}
      exit={{ y: 50, opacity: 0 }}
      transition={{ duration: 0.5 }}
      className="message-panel"
    >
      <span>{message}</span>
      <button className="close-button" onClick={() => setIsVisible(false)}>
        <X size={20} className="icon" />
      </button>
    </motion.div>
  );
}

export default MessagePanel;