import { useNavigate } from "react-router-dom";
import ChatCard from "../../../components/cards/ChatCard/ChatCard";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import styles from "./index.module.scss";

const LeftColumn = () => {
  const navigator = useNavigate();
  const { conversations } = useTypedSelector((store) => store.conversation);

  const onConversationClickHandle = (guid: string) => {
    navigator(guid);
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
                title={item.name}
                lastMessage={item.lastMessage?.message}
                badgeNumber={0}
                time={item.lastMessage?.dateTime}
                image={item.image}
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
