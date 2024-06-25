import ChatCard from "../../../components/cards/ChatCard/ChatCard";
import styles from "./index.module.scss";

const LeftColumn = () => {
  return (
    <>
      <aside className={styles.left_column}>
        <div className={styles.left_column_main}>
          <header className={styles.left_column_main_header}>Search</header>
          <div className={styles.left_column_main_list}>
            <ChatCard
              title="INVASION"
              lastMessage="рашисты так радуются возможному сбитию американского БПЛА - вы там поясните жите..."
              badge_number={3}
              time="Mon"
            />
            <ChatCard
              title="INVASION"
              lastMessage="рашисты так радуются возможному сбитию американского БПЛА - вы там поясните жите..."
              badge_number={3}
              time="Mon"
            />
            <ChatCard
              title="INVASION"
              lastMessage="рашисты так радуются возможному сбитию американского БПЛА - вы там поясните жите..."
              badge_number={3}
              time="Mon"
            />
          </div>
        </div>
      </aside>
    </>
  );
};

export default LeftColumn;
