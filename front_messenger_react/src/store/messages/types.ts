export enum MessageTypes {
  Text,
  File,
  Image,
  Video,
}

export interface IMessageItem {
  // id: number;
  guid: string;
  message: string;
  senderId?: number;
  // conversationId: string;
  conversationGuid: string;
  messageType: MessageTypes;
  dateTime: Date;
  isPending?: boolean;
}

export interface IMessageSendItem {
  message: string;
  // conversationId: number;
  conversationGuid: string;
  messageType: MessageTypes;
}

export interface IMessageState {
  // loading: boolean;
  conversationGuid: string;
  messages: Array<IMessageItem>;
}

export enum MessageActionTypes {
  SET_MESSAGE_PAGE = "SET_MESSAGE_PAGE",
  SEND_MESSAGE_HTTP = "SEND_MESSAGE_HTTP",

  RECEIVE_MESSAGE = "RECEIVE_MESSAGE",
  SEND_MESSAGE_PENDING = "SEND_MESSAGE_PENDING",
  SEND_MESSAGE_SUCCESS = "SEND_MESSAGE_SUCCESS",
}

export interface SetMessagePageAction {
  type: MessageActionTypes.SET_MESSAGE_PAGE;
  payload: IMessageState;
}

export interface SendMessageHTTPAction {
  type: MessageActionTypes.SEND_MESSAGE_HTTP;
  payload: { message: IMessageItem };
}

export interface ReceiveMessageAction {
  type: MessageActionTypes.RECEIVE_MESSAGE;
  payload: { message: IMessageItem };
}

export interface SendMessagePendingAction {
  type: MessageActionTypes.SEND_MESSAGE_PENDING;
  payload: { message: IMessageItem };
}

export interface SendMessageSuccessAction {
  type: MessageActionTypes.SEND_MESSAGE_SUCCESS;
  payload: { tempMessageGuid: string };
}

export type MessageActions =
  | SetMessagePageAction
  | SendMessageHTTPAction
  | ReceiveMessageAction
  | SendMessagePendingAction
  | SendMessageSuccessAction;
