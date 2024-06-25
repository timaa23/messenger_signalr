import { connection } from "../components/containers/default/DefaultLayout";
import { useActions } from "./useActions";

export const useChatService = () => {
  const { SetLastMessage } = useActions();

  const start = async () => {
    try {
      await connection.start();
      console.log("SignalR Connected.");

      connection.on(
        "ReceiveMessage",
        (user_name: string, message: string, date_time: string) => {
          SetLastMessage({ date_time, message, user_name });
          console.log("Receive", { user_name, message, date_time });
        }
      );

      connection.on("ReceiveConnectedUsers", (users: Array<string>) => {
        console.log("ReceiveUsers", users);
      });
    } catch (error) {
      console.error(error);
      setTimeout(start, 5000);
    }
  };

  const connectSocket = async (name: string, room: string) => {
    return connection.invoke("OnConnectedAsync", { user: name, room });
  };

  const testSocket = async (name: string, room: string) => {
    return connection.invoke("TestAsync", { user: name, room });
  };

  const sendMessage = async (message: string) => {
    return connection.invoke("SendMessage", message);
  };

  const disconnectSocket = async () => {
    return connection.stop();
  };

  return { start, connectSocket, disconnectSocket, sendMessage, testSocket };
};
