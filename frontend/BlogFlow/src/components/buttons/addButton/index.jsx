
import "./style.css"

function AddButton(props) {
  return (
    <button
      title={props.title}
      type="button"
      className="add-button-container"
      onClick={props.onclick}
    ></button>
  );
}

export default AddButton;