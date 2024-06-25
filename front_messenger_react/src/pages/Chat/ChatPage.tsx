import LeftColumn from "./LeftColumn/LeftColumn";
import MiddleColumn from "./MiddleColumn/MiddleColumn";
import styles from "./index.module.scss";

const ChatPage = () => {
  return (
    <>
      <div className={styles.main}>
        <LeftColumn />
        <MiddleColumn />
      </div>
    </>
  );
};

export default ChatPage;
