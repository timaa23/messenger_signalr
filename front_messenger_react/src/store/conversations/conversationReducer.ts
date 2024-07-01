import moment from "moment";
import {
  ConversationActionTypes,
  ConversationActions,
  IConversationItem,
  IConversationState,
} from "./types";

const initialState: IConversationState = {
  conversations: [],
  // currentConversation: "",
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

      var conversation = conversations.find((c) => c.guid === message.conversationGuid);
      if (!conversation) return { ...state };

      conversation.lastMessage = message;

      // sorting by last message date with momentJs
      sortByDate(conversations);

      return { ...state };
    }
    default:
      return state;
  }
};

const sortByDate = (array: Array<IConversationItem>) => {
  array.sort((l, r) => moment(r.lastMessage?.dateTime).diff(l.lastMessage?.dateTime));
};
