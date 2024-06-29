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
  //   clickCallback(guid: string): void;
}

const MessageCard: React.FC<MessageCardProps> = ({
  owner,
  message,
  //   conversationGuid,
  //   messageType,
  dateTime,
}) => {
  return (
    <>
      <div className={classNames(styles.message_wrapper, { [styles.own]: owner })}>
        <div className={styles.message}>
          <div className={styles.content}>
            <div className={styles.content_text}>
              {message}
              <span className={styles.time_meta}>{moment(dateTime).format("LT")}</span>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default MessageCard;
