import React from "react";
import PFP from "../../../assets/pfp.jpg";
import styles from "./index.module.scss";

interface ChatCardProps {
  title: string;
  time: string;
  lastMessage: string;
  badge_number: string | number;
}

const ChatCard: React.FC<ChatCardProps> = ({
  title,
  time,
  lastMessage,
  badge_number,
}) => {
  return (
    <>
      <div className={styles.card}>
        <div className={styles.card_button}>
          <div className={styles.card_status}>
            <div className={styles.card_status_avatar}>
              <div className={styles.card_status_avatar_inner}>
                <img src={PFP} alt="pfp" />
              </div>
            </div>
          </div>
          <div className={styles.card_info}>
            <div className={styles.card_info_row}>
              <div className={styles.card_info_row_title}>
                <h3>{title}</h3>
              </div>
              <div className={styles.card_info_row_time_meta}>
                <span className={styles.time}>{time}</span>
              </div>
            </div>
            <div className={styles.card_info_subtitle}>
              <p className={styles.card_info_subtitle_last_message}>
                <span>{lastMessage}</span>
              </p>
              <div>
                <div className={styles.card_info_subtitle_badge}>
                  <span>{badge_number}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default ChatCard;
