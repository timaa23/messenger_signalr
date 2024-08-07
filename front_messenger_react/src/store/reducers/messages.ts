import {
  IMessageItem,
  IMessageState,
  MessageActionTypes,
  MessageActions,
} from "../types/messages";

const initialState: IMessageState = {
  loading: false,
  conversationId: 0,
  messages: [],
};

export const MessageReducer = (
  state = initialState,
  action: MessageActions
): IMessageState => {
  switch (action.type) {
    // Fetch message
    case MessageActionTypes.FETCH_MESSAGES_PENDING: {
      return {
        ...state,
        loading: true,
        messages: [],
        conversationId: action.payload.conversationId,
      };
    }
    case MessageActionTypes.FETCH_MESSAGES_SUCCESS: {
      return {
        ...state,
        loading: false,
        messages: action.payload.messages,
      };
    }
    case MessageActionTypes.FETCH_MESSAGES_FAILURE: {
      return {
        ...state,
        loading: false,
        messages: [],
        conversationId: 0,
      };
    }

    // Send message
    case MessageActionTypes.SEND_MESSAGE_PENDING: {
      return {
        ...state,
        messages: [action.payload.message, ...state.messages],
      };
    }
    case MessageActionTypes.SEND_MESSAGE_SUCCESS: {
      const { messages, conversationId } = state;
      const { message, tempMessageGuid } = action.payload;

      if (message.conversationId !== conversationId) return { ...state };

      const messagesTemp = replacePendingMessage(messages, message, tempMessageGuid);

      return { ...state, messages: messagesTemp };
    }

    // Receive messsage
    case MessageActionTypes.RECEIVE_MESSAGE: {
      const { messages, conversationId } = state;
      const { message } = action.payload;

      if (message.conversationId !== conversationId) return { ...state };

      const messagesTemp = setReceivedMessage(messages, message);

      return { ...state, messages: messagesTemp };
    }
    default:
      return state;
  }
};

const setReceivedMessage = (messages: Array<IMessageItem>, message: IMessageItem) => {
  var lastSuccessId = messages.findIndex((m) => !m.isPending);
  if (lastSuccessId == -1) lastSuccessId = messages.length;

  var list = messages.slice(0);
  // set message after last success
  list.splice(lastSuccessId, 0, message);

  return list;
};

const replacePendingMessage = (
  messages: Array<IMessageItem>,
  message: IMessageItem,
  messageGuid: string
) => {
  const pendingMessages = [];
  const nonPendingMessages = [];

  for (const item of messages) {
    if (item.guid === messageGuid) continue;

    if (item.isPending) pendingMessages.push(item);
    else nonPendingMessages.push(item);
  }

  return [...pendingMessages, message, ...nonPendingMessages];
};
