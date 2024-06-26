import { useEffect } from "react";
import { useActions } from "../../hooks/useActions";
import LeftColumn from "./LeftColumn/LeftColumn";
import MiddleColumn from "./MiddleColumn/MiddleColumn";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";

const ChatPage = () => {
  const { GetConversations } = useActions();
  const { guid } = useParams();

  const LoadConversations = async () => {
    try {
      await GetConversations();
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
    }
  };

  useEffect(() => {
    LoadConversations();
  }, []);

  return (
    <>
      <div className={styles.main}>
        <LeftColumn />
        {guid && <MiddleColumn />}
      </div>
    </>
  );
};

export default ChatPage;
