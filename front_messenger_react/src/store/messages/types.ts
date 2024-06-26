export enum MessageTypes {
  Text,
  File,
  Image,
  Video,
}

export interface IMessageItem {
  senderId: number;
  message: string;
  conversationGuid: string;
  messageType: MessageTypes;
  dateTime: Date;
}

export interface IMessageSendItem {
  senderId: number;
  message: string;
  conversationGuid: string;
  messageType: MessageTypes;
}

export interface IReceiveMessageType {
  user_name: string;
  message: string;
  date_time: string;
}

export interface IReceiveMessageState {
  last_message: IReceiveMessageType;
}

export enum MessageActionTypes {
  SET_LAST_MESSAGE = "SET_LAST_MESSAGE",
}

export interface SetLastMessageAction {
  type: MessageActionTypes.SET_LAST_MESSAGE;
  payload: IReceiveMessageState;
}

export type MessageActions = SetLastMessageAction;
