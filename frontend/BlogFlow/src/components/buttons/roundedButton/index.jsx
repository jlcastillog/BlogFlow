
import "./style.css"

/*
* Component for a rounded button
* @param {object} props
* @param {string} props.title 
* @param {string} props.type 
* - add
* - edit
* - remove
* @param {function} props.onclick
*/
function RoundedButton(props) {
  return (
    <button
      title={props.title}
      type="button"
      className={`rounded-button-container ${props.type}`}
      onClick={props.onclick}
    ></button>
  );
}

export default RoundedButton;