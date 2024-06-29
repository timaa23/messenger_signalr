import * as signalR from "@microsoft/signalr";
import { IMessageSendItem } from "../store/messages/types";

const SIGNALR_URL: string = "http://localhost:5010/chat";

class ChatServiceConnector {
  static instance: ChatServiceConnector;
  private connection: signalR.HubConnection;

  public events: (
    onMessageReceived: (username: string, message: string, date: Date) => void
  ) => void;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(SIGNALR_URL, { accessTokenFactory: () => localStorage.token })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.start();

    this.events = (onMessageReceived) => {
      this.connection.on("ReceiveMessage", (username, message, date) => {
        onMessageReceived(username, message, date);
      });
    };
  }

  private async start() {
    try {
      await this.connection.start();
      console.log("Connection has established");
    } catch (error) {
      console.error("error", error);
    }
  }

  public sendMessage = (model: IMessageSendItem) => {
    return this.connection.invoke("SendMessage", model);
  };

  public testMessage = (message: string, guid: string) => {
    return this.connection.invoke("TestAsync", message, guid);
  };

  public static getInstance(): ChatServiceConnector {
    if (!ChatServiceConnector.instance)
      ChatServiceConnector.instance = new ChatServiceConnector();
    return ChatServiceConnector.instance;
  }
}

export default ChatServiceConnector.getInstance;
