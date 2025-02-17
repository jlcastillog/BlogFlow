
import "./style.css"

function RemoveButton(props) {
  return (
    <button
      title={props.title}
      type="button"
      className="remove-button-container"
      onClick={props.onclick}
    ></button>
  );
}

export default RemoveButton;
