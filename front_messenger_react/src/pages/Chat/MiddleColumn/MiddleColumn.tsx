import { useNavigate, useParams } from "react-router-dom";
import { useActions } from "../../../hooks/useActions";
import styles from "./index.module.scss";
import { useEffect } from "react";
import { useTypedSelector } from "../../../hooks/useTypedSelector";
import MessageCard from "../../../components/cards/MessageCard/MessageCard";
import classNames from "classnames";
import MessageInput from "../../../components/common/inputs/MessageInput/MessageInput";
import { IMessageSendItem, MessageTypes } from "../../../store/messages/types";
import { useFormik } from "formik";
import * as Yup from "yup";

const MiddleColumn = () => {
  const { SetMessagePage, SendMessage } = useActions();
  const { guid } = useParams();

  const { user } = useTypedSelector((store) => store.auth);
  const { messages } = useTypedSelector((store) => store.message);

  const navigator = useNavigate();

  const LoadMessages = async () => {
    try {
      await SetMessagePage(guid ?? "");
      console.log("SET MESSAGES");
    } catch (error: any) {
      console.error("Щось пішло не так, ", error);
      navigator("/");
    }
  };

  useEffect(() => {
    LoadMessages();
  }, [guid]);

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
    conversationGuid: guid ?? "",
    messageType: MessageTypes.Text,
  };

  const sendSchema = Yup.object().shape({
    message: Yup.string().required("*Required"),
  });

  const formik = useFormik<IMessageSendItem>({
    initialValues: modelInitValues,
    validationSchema: sendSchema,
    onSubmit: onSubmitHandler,
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
