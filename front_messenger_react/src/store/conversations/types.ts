import { IMessageItem } from "../messages/types";

export interface IConversationItem {
  guid: string;
  name: string;
  image: string;
  lastMessage: IMessageItem | null;
}

export interface IConversationState {
  // currentConversation: string;
  conversations: Array<IConversationItem>;
}

export enum ConversationActionTypes {
  GET_USER_CONVERSATIONS = "GET_CONVERSATIONS",
}

export interface GetUserConversationsAction {
  type: ConversationActionTypes.GET_USER_CONVERSATIONS;
  payload: IConversationState;
}

export type ConversationActions = GetUserConversationsAction;
