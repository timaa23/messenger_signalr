import {
  ConversationActionTypes,
  ConversationActions,
  IConversationItem,
  IConversationState,
} from "../types/conversations";
import { IMessageItem } from "../types/messages";

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
    if (conversation.id === message.conversationId) {
      return { ...conversation, lastMessage: { ...message } };
    }

    return conversation;
  });

  return list;
};
