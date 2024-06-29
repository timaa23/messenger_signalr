import { useEffect } from "react";
import { useActions } from "../../hooks/useActions";
import LeftColumn from "./LeftColumn/LeftColumn";
import MiddleColumn from "./MiddleColumn/MiddleColumn";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";
import chatServiceConnector from "../../helpers/chatServiceConnector";

const ChatPage = () => {
  const { GetConversations } = useActions();
  const { guid } = useParams();

  const { testMessage, events } = chatServiceConnector();

  const LoadConversations = async () => {
    try {
      await GetConversations();
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
    }
  };

  useEffect(() => {
    LoadConversations();

    events((userName, message, date) => {
      console.log("OBJECT WS IN CHAT PAGE", { userName, message, date });
    });
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
