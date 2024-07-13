import { IMessageItem } from "./messages";
import { IParticipantItem } from "./participants";

export enum ConversationTypes {
  Single,
  Group,
}

export interface IConversationItem {
  id: number;
  name: string;
  image: string;
  lastMessage: IMessageItem | null;
  conversationType: ConversationTypes;
  participants: Array<IParticipantItem>;
}

export interface IConversationViewItem {
  id: number;
  name: string;
  image: string;
  lastMessage: IMessageItem | null;
  conversationType: ConversationTypes;
}

export interface IConversationState {
  conversations: Array<IConversationItem>;
}

export enum ConversationActionTypes {
  GET_USER_CONVERSATIONS = "GET_USER_CONVERSATIONS",
  RECEIVE_MESSAGE_CONVERSATION = "RECEIVE_MESSAGE_CONVERSATION",
}

export interface GetUserConversationsAction {
  type: ConversationActionTypes.GET_USER_CONVERSATIONS;
  payload: IConversationState;
}

export interface ReceiveMessageConversationAction {
  type: ConversationActionTypes.RECEIVE_MESSAGE_CONVERSATION;
  payload: { message: IMessageItem };
}

export type ConversationActions =
  | GetUserConversationsAction
  | ReceiveMessageConversationAction;
