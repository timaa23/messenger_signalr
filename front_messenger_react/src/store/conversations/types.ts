import { IMessageItem } from "../messages/types";
import { IParticipantItem } from "../participant/types";

export enum ConversationTypes {
  Single,
  Group,
}

export interface IConversationItem {
  guid: string;
  name: string;
  image: string;
  lastMessage: IMessageItem | null;
  conversationType: ConversationTypes;
  participants: Array<IParticipantItem>;
}

export interface IConversationState {
  // currentConversation: string;
  conversations: Array<IConversationItem>;
}

export enum ConversationActionTypes {
  GET_USER_CONVERSATIONS = "GET_USER_CONVERSATIONS",
}

export interface GetUserConversationsAction {
  type: ConversationActionTypes.GET_USER_CONVERSATIONS;
  payload: IConversationState;
}

export type ConversationActions = GetUserConversationsAction;
