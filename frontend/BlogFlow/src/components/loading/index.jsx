import "./style.css";

function Loading({text}) {
  return (
    <li className="skeleton-loader">
      <p>{text}</p>
    </li>
  );
}

export default Loading;