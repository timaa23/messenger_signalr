import React from "react";
import styles from "./index.module.scss";
import moment from "moment";
import { useTypedSelector } from "../../../hooks/useTypedSelector";

interface ChatCardProps {
  guid: string;
  title: string;
  time?: Date;
  lastMessage?: string;
  badgeNumber: number;
  image?: string;
  senderId?: number;
  clickCallback(guid: string): void;
}

const ChatCard: React.FC<ChatCardProps> = ({
  guid,
  title,
  time,
  lastMessage,
  badgeNumber,
  image,
  senderId,
  clickCallback,
}) => {
  const { user } = useTypedSelector((store) => store.auth);

  const onClickHandle = () => {
    clickCallback(guid);
  };

  return (
    <>
      <div onClick={() => onClickHandle()} className={styles.card}>
        <div className={styles.card_button}>
          <div className={styles.card_status}>
            <div className={styles.card_status_avatar}>
              <div className={styles.card_status_avatar_inner}>
                {image ? <img src={image} alt="pfp" /> : title[0]}
              </div>
            </div>
          </div>
          <div className={styles.card_info}>
            <div className={styles.card_info_row}>
              <div className={styles.card_info_row_title}>
                <h3>{title}</h3>
              </div>
              <div className={styles.card_info_row_time_meta}>
                <span className={styles.time}>{moment(time).format("hh:mm")}</span>
              </div>
            </div>
            <div className={styles.card_info_subtitle}>
              <p className={styles.card_info_subtitle_last_message}>
                {user?.id == senderId && (
                  <span className={styles.sender_name}>You: </span>
                )}
                <span>{lastMessage ?? "No messages here yet..."}</span>
              </p>
              {badgeNumber > 0 && (
                <div>
                  <div className={styles.card_info_subtitle_badge}>
                    <span>{badgeNumber}</span>
                  </div>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ChatCard;
