
import style from "./style.css"

function RoundedButton(props) {
  return (
    <button
      title={props.title}
      type="button"
      className={`{style.rounded-button-container ${props.type}`}
      onClick={props.onclick}
    ></button>
  );
}

export default RoundedButton;