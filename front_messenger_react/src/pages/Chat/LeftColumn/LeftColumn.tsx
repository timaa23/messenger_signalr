import { useNavigate } from "react-router-dom";
import ChatCard from "../../../components/cards/ChatCard/ChatCard";
import styles from "./index.module.scss";
import { selectConversationList } from "../../../store/selectors/conversations";
import { useSelector } from "react-redux";
import { useTypedParams } from "../../../hooks/useTypedParams";

const LeftColumn = () => {
  const conversationIdParam = useTypedParams("conversationId", "number");
  const list = useSelector(selectConversationList);

  const navigator = useNavigate();

  const onConversationClickHandle = (id: number) => {
    if (conversationIdParam === id) return;

    navigator(`/${id}`);
  };

  return (
    <>
      <aside className={styles.left_column}>
        <div className={styles.left_column_main}>
          <header className={styles.left_column_main_header}>Search</header>
          <div className={styles.left_column_main_list}>
            {list.map((item) => (
              <ChatCard
                key={item.id}
                id={item.id}
                title={item.name}
                lastMessage={item.lastMessage?.message}
                time={item.lastMessage?.dateTime}
                image={item.image}
                senderId={item.lastMessage?.senderId}
                clickCallback={onConversationClickHandle}
                isActive={item.id === conversationIdParam}
              />
            ))}
          </div>
        </div>
      </aside>
    </>
  );
};

export default LeftColumn;
