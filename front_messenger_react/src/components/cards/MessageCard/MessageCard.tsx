import classNames from "classnames";
// import { MessageTypes } from "../../../store/messages/types";
import styles from "./index.module.scss";
import moment from "moment";

interface MessageCardProps {
  owner: boolean;
  message: string;
  //   conversationGuid: string;
  //   messageType: MessageTypes;
  dateTime: Date;
  isPending?: boolean;
  //   clickCallback(guid: string): void;
}

const MessageCard: React.FC<MessageCardProps> = ({
  owner,
  message,
  //   conversationGuid,
  //   messageType,
  dateTime,
  isPending,
}) => {
  const StatusIcon = () => {
    var icon_class = "";
    if (isPending) icon_class = "icon-message-pending";
    else icon_class = "icon-message-succeeded";

    return <i className={classNames(styles.message_status, "icon", icon_class)} />;
  };

  return (
    <>
      <div className={classNames(styles.message_wrapper, { [styles.own]: owner })}>
        <div className={styles.message}>
          <div className={styles.content}>
            <div className={styles.content_text}>
              {message}
              <div className={styles.content_text_meta}>
                <span className={styles.time_meta}>{moment(dateTime).format("LT")}</span>
                {owner && <StatusIcon />}
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default MessageCard;
