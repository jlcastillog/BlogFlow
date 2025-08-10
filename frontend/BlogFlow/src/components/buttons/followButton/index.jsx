import "./style.css";

/**
 * @param {object} props
 * @param {boolean} props.isFollowing
 * @param {function} props.onClick
 */
function FollowButton({ isFollowing, onClick }) {
  return (
    <button
      className={`follow-button ${isFollowing ? "following" : ""}`}
      onClick={onClick}
    >
      {isFollowing ? "Unfollow" : "Follow"}
    </button>
  );
}

export default FollowButton;