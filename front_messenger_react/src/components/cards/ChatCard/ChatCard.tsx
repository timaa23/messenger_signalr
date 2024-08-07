import React from "react";
import styles from "./index.module.scss";
import moment from "moment";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import classNames from "classnames";

interface ChatCardProps {
  id: number;
  title: string;
  time?: Date;
  lastMessage?: string;
  badgeNumber?: number;
  isActive?: boolean;
  image?: string;
  senderId?: number;
  clickCallback(id: number): void;
}

const ChatCard: React.FC<ChatCardProps> = ({
  id,
  title,
  time,
  lastMessage,
  badgeNumber,
  isActive,
  image,
  senderId,
  clickCallback,
}) => {
  const { user } = useTypedSelector((store) => store.auth);

  const onClickHandle = () => {
    clickCallback(id);
  };

  return (
    <>
      <div
        onClick={() => onClickHandle()}
        className={classNames(styles.card, { [styles.active]: isActive })}
      >
        <div className={styles.card_button}>
          <div className={styles.card_status}>
            <div className={styles.avatar}>
              <div className={styles.avatar_inner}>
                {image ? <img src={image} alt="pfp" /> : title[0]}
              </div>
            </div>
          </div>
          <div className={styles.card_info}>
            <div className={styles.row}>
              <div className={styles.row_title}>
                <h3>{title}</h3>
              </div>
              <div className={styles.row_time_meta}>
                <span className={styles.time}>{time && moment(time).format("LT")}</span>
              </div>
            </div>
            <div className={styles.subtitle}>
              <p className={styles.subtitle_last_message}>
                {user?.id == senderId && (
                  <span className={styles.sender_name}>You: </span>
                )}
                <span>{lastMessage ?? "No messages here yet..."}</span>
              </p>
              {badgeNumber && badgeNumber > 0 ? (
                <div>
                  <div className={styles.subtitle_badge}>
                    <span>{badgeNumber}</span>
                  </div>
                </div>
              ) : null}
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ChatCard;
