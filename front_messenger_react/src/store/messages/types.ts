export enum MessageTypes {
  Text,
  File,
  Image,
  Video,
}

export interface IMessageItem {
  senderId: number;
  message: string;
  guid: string;
  conversationGuid: string;
  messageType: MessageTypes;
  dateTime: Date;
}

export interface IMessageSendItem {
  // senderId: number;
  message: string;
  conversationGuid: string;
  messageType: MessageTypes;
}

export interface IMessageState {
  loading: boolean;
  messages: Array<IMessageItem>;
}

export enum MessageActionTypes {
  SET_MESSAGE_PAGE = "SET_MESSAGE_PAGE",
  SEND_MESSAGE_HTTP = "SEND_MESSAGE_HTTP",

  //IN PROGRESS
  SEND_MESSAGE = "SEND_MESSAGE",
}

export interface SetMessagePageAction {
  type: MessageActionTypes.SET_MESSAGE_PAGE;
  payload: IMessageState;
}

export interface SendMessageAction {
  type: MessageActionTypes.SEND_MESSAGE_HTTP;
  payload: { message: IMessageItem };
}

export type MessageActions = SetMessagePageAction | SendMessageAction;
