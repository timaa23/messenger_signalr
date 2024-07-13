import { useNavigate } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import styles from "./index.module.scss";
import { useEffect, useRef } from "react";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import MessageCard from "../../../components/cards/MessageCard/MessageCard";
import classNames from "classnames";
import MessageInput from "../../../components/common/inputs/MessageInput/MessageInput";
import { IMessageSendItem, MessageTypes } from "../../../store/types/messages";
import { useFormik } from "formik";
import { invariant } from "../../../helpers/invariant";
import * as Yup from "yup";
import { useTypedParams } from "../../../hooks/useTypedParams";

const MiddleColumn = () => {
  const { FetchMessages, SendMessage } = useActions();
  const conversationIdParam = useTypedParams("conversationId", "number");

  invariant(conversationIdParam);

  const { user } = useTypedSelector((store) => store.auth);
  const { messages } = useTypedSelector((store) => store.message);

  const navigator = useNavigate();

  const scrollToRef = useRef<HTMLDivElement>(null);

  const LoadMessages = async () => {
    try {
      await FetchMessages(conversationIdParam);
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
      navigator("/");
    }
  };

  const ScrollToBottom = () => {
    scrollToRef.current?.scrollIntoView();
  };

  useEffect(() => {
    console.log("CONV ID", conversationIdParam);

    LoadMessages();
  }, [conversationIdParam]);

  useEffect(() => {
    ScrollToBottom();
  }, [messages]);

  const onSubmitHandler = async (model: IMessageSendItem) => {
    setFieldValue("message", "");

    try {
      await SendMessage(model);
    } catch (error) {
      console.error("Щось пішло не так, ", error);
    }
  };

  //Formik
  const modelInitValues: IMessageSendItem = {
    message: "",
    conversationId: conversationIdParam,
    messageType: MessageTypes.Text,
  };

  const sendSchema = Yup.object().shape({
    message: Yup.string().required("*Required"),
  });

  const formik = useFormik<IMessageSendItem>({
    initialValues: modelInitValues,
    validationSchema: sendSchema,
    onSubmit: onSubmitHandler,
    enableReinitialize: true,
  });

  const { values, handleSubmit, handleChange, setFieldValue } = formik;

  return (
    <>
      <main className={styles.middle_column}>
        <div className={styles.middle_column_main}>
          <header className={styles.middle_column_main_header}>User</header>
          <div className={classNames(styles.messages, "custom-scroll")}>
            <div className={styles.message_list}>
              {messages.map((item) => (
                <MessageCard
                  key={item.guid}
                  message={item.message}
                  owner={user?.id == item.senderId || !!item.isPending}
                  dateTime={item.dateTime}
                  isPending={item.isPending}
                />
              ))}
            </div>
            <div ref={scrollToRef} />
          </div>
          <div className={styles.middle_column_main_footer}>
            <form onSubmit={handleSubmit}>
              <MessageInput
                id="message"
                name="message"
                autoComplete="off"
                type="text"
                placeholder="Message"
                value={values.message}
                onChange={handleChange}
              />
            </form>
          </div>
        </div>
      </main>
    </>
  );
};

export default MiddleColumn;
