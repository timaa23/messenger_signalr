import { useNavigate, useParams } from "react-router-dom";
import ChatCard from "../../../components/cards/ChatCard/ChatCard";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import styles from "./index.module.scss";
import { ConversationTypes, IConversationItem } from "../../../store/conversations/types";

const LeftColumn = () => {
  const { guid } = useParams();

  const { user } = useTypedSelector((store) => store.auth);
  const { conversations } = useTypedSelector((store) => store.conversation);

  const navigator = useNavigate();

  const onConversationClickHandle = (conversationGuid: string) => {
    if (guid === conversationGuid) return;

    navigator(`/${conversationGuid}`);
  };

  const getCardFields = (model: IConversationItem) => {
    if (model.conversationType === ConversationTypes.Group)
      return { name: model.name, image: model.image };

    var participant = model.participants.find((p) => p.id != user?.id);
    return {
      name: participant?.name ?? "",
      image: participant?.image ?? "",
    };
  };

  return (
    <>
      <aside className={styles.left_column}>
        <div className={styles.left_column_main}>
          <header className={styles.left_column_main_header}>Search</header>
          <div className={styles.left_column_main_list}>
            {conversations.map((item) => (
              <ChatCard
                key={item.guid}
                guid={item.guid}
                title={getCardFields(item).name}
                lastMessage={item.lastMessage?.message}
                time={item.lastMessage?.dateTime}
                image={getCardFields(item).image}
                senderId={item.lastMessage?.senderId}
                clickCallback={onConversationClickHandle}
              />
            ))}
          </div>
        </div>
      </aside>
    </>
  );
};

export default LeftColumn;
