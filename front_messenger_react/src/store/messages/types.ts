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
  loading: boolean;
  conversationGuid: string;
  messages: Array<IMessageItem>;
}

export enum MessageActionTypes {
  FETCH_MESSAGES_PENDING = "FETCH_MESSAGES_PENDING",
  FETCH_MESSAGES_SUCCESS = "FETCH_MESSAGES_SUCCESS",
  FETCH_MESSAGES_FAILURE = "FETCH_MESSAGES_FAILURE",

  RECEIVE_MESSAGE = "RECEIVE_MESSAGE",

  SEND_MESSAGE_PENDING = "SEND_MESSAGE_PENDING",
  SEND_MESSAGE_SUCCESS = "SEND_MESSAGE_SUCCESS",

  // SEND_MESSAGE_HTTP = "SEND_MESSAGE_HTTP",
}

// Fetch message
export interface FetchMessagesPendingAction {
  type: MessageActionTypes.FETCH_MESSAGES_PENDING;
  payload: { conversationGuid: string };
}

export interface FetchMessagesSuccessAction {
  type: MessageActionTypes.FETCH_MESSAGES_SUCCESS;
  payload: { messages: Array<IMessageItem> };
}

export interface FetchMessagesFailureAction {
  type: MessageActionTypes.FETCH_MESSAGES_FAILURE;
}

// Send message
export interface SendMessagePendingAction {
  type: MessageActionTypes.SEND_MESSAGE_PENDING;
  payload: { message: IMessageItem };
}

export interface SendMessageSuccessAction {
  type: MessageActionTypes.SEND_MESSAGE_SUCCESS;
  payload: { message: IMessageItem; tempMessageGuid: string };
}

// Receive messsage
export interface ReceiveMessageAction {
  type: MessageActionTypes.RECEIVE_MESSAGE;
  payload: { message: IMessageItem };
}

// Action types
export type MessageActions =
  | FetchMessagesPendingAction
  | FetchMessagesSuccessAction
  | FetchMessagesFailureAction
  | SendMessagePendingAction
  | SendMessageSuccessAction
  | ReceiveMessageAction;
// | SendMessageHTTPAction;

// JUST FOR TESTING
// export interface SendMessageHTTPAction {
//   type: MessageActionTypes.SEND_MESSAGE_HTTP;
//   payload: { message: IMessageItem };
// }
