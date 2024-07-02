import { useEffect } from "react";
import { useActions } from "../../hooks/useActions";
import LeftColumn from "./LeftColumn/LeftColumn";
import MiddleColumn from "./MiddleColumn/MiddleColumn";
import styles from "./index.module.scss";
import { useParams } from "react-router-dom";
import chatServiceConnector from "../../helpers/chatServiceConnector";
import ServiceResponse from "../../ServiceResponse";
import { IMessageItem } from "../../store/messages/types";
import { useTypedSelector } from "../../hooks/useTypedSelector";

const ChatPage = () => {
  const { GetConversations, ReceiveMessage, ReceiveMessageConversation } = useActions();
  const { guid } = useParams();

  const { user } = useTypedSelector((store) => store.auth);

  const { events } = chatServiceConnector();

  const LoadConversations = async () => {
    try {
      await GetConversations();
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
    }
  };

  const onReceiveMessage = async (model: ServiceResponse<IMessageItem>) => {
    try {
      if (user?.id != model.payload.senderId) {
        // Set message in middle column
        await ReceiveMessage(model);
      }

      // Set message in left column
      await ReceiveMessageConversation(model);
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
    }
  };

  useEffect(() => {
    LoadConversations();

    events((model) => {
      console.log("OBJECT WS IN CHAT PAGE", model);
      onReceiveMessage(model);
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
