import moment from "moment";
import {
  ConversationActionTypes,
  ConversationActions,
  IConversationItem,
  IConversationState,
} from "./types";
import { IMessageItem } from "../messages/types";

const initialState: IConversationState = {
  conversations: [],
};

export const ConversationReducer = (
  state = initialState,
  action: ConversationActions
): IConversationState => {
  switch (action.type) {
    case ConversationActionTypes.GET_USER_CONVERSATIONS: {
      return {
        ...state,
        ...action.payload,
      };
    }
    case ConversationActionTypes.RECEIVE_MESSAGE_CONVERSATION: {
      const { conversations } = state;
      const { message } = action.payload;

      const conversationsTemp = setLastMessage(conversations, message);

      // sorting by last message date with momentJs
      sortByDate(conversationsTemp);

      return { ...state, conversations: conversationsTemp };
    }
    default:
      return state;
  }
};

const setLastMessage = (
  conversations: Array<IConversationItem>,
  message: IMessageItem
) => {
  // setting last message in conversation
  var list = conversations.map((conversation) => {
    if (conversation.guid === message.conversationGuid) {
      return { ...conversation, lastMessage: { ...message } };
    }
    return conversation;
  });

  return list;
};

const sortByDate = (array: Array<IConversationItem>) => {
  array.sort((l, r) => moment(r.lastMessage?.dateTime).diff(l.lastMessage?.dateTime));
};
