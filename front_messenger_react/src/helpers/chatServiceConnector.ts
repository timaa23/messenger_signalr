import * as signalR from "@microsoft/signalr";
import { IMessageItem, IMessageSendItem } from "../store/messages/types";
import ServiceResponse from "../ServiceResponse";

const SIGNALR_URL: string = "http://localhost:5010/chat";

class ChatServiceConnector {
  static instance: ChatServiceConnector;
  private connection: signalR.HubConnection;

  public events: (
    onMessageReceived: (model: ServiceResponse<IMessageItem>) => void
  ) => void;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(SIGNALR_URL, { accessTokenFactory: () => localStorage.token })
      .withAutomaticReconnect()
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.start();

    this.events = (onMessageReceived) => {
      this.connection.on("ReceiveMessage", (model: ServiceResponse<IMessageItem>) => {
        onMessageReceived(model);
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

  public sendMessage = (
    model: IMessageSendItem
  ): Promise<ServiceResponse<IMessageItem>> => {
    return this.connection.invoke("SendMessage", model);
  };

  public static getInstance(): ChatServiceConnector {
    if (!ChatServiceConnector.instance)
      ChatServiceConnector.instance = new ChatServiceConnector();
    return ChatServiceConnector.instance;
  }
}

export default ChatServiceConnector.getInstance;
