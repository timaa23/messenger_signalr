import React, { FormEvent, InputHTMLAttributes } from "react";
import styles from "./index.module.scss";

interface MessageInputProps extends InputHTMLAttributes<HTMLInputElement> {
  clickCallback?: () => void;
}

const MessageInput: React.FC<MessageInputProps> = ({ className, ...props }) => {
  return (
    <>
      <div className={styles.message_input_wrapper}>
        <input className={styles.message_input} {...props} />
        <button type="submit" className={styles.send_button}>
          <i className="icon icon-send" />
        </button>
      </div>
    </>
  );
};

export default MessageInput;
