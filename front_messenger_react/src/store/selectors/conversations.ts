import moment from "moment";
import { createTypedSelector } from "..";
import {
  ConversationTypes,
  IConversationItem,
  IConversationViewItem,
} from "../types/conversations";

export const selectConversationList = createTypedSelector([(state) => state], (state) => {
  const { user } = state.auth;
  const { conversations } = state.conversation;

  var list: IConversationViewItem[] = [];

  conversations.map((conversation) => {
    const { image, name } = getConversationInfo(conversation, user?.id);

    list.push({
      id: conversation.id,
      conversationType: conversation.conversationType,
      lastMessage: conversation.lastMessage,
      image: image,
      name: name,
    });
  });

  return toSortedByDate(list);
});

const getConversationInfo = (model: IConversationItem, userId?: number) => {
  if (model.conversationType === ConversationTypes.Group)
    return { name: model.name, image: model.image };

  var participant = model.participants.find((p) => p.id != userId);
  return {
    name: participant?.name ?? "",
    image: participant?.image ?? "",
  };
};

const toSortedByDate = (array: Array<IConversationViewItem>) => {
  return array.toSorted((l, r) =>
    moment(r.lastMessage?.dateTime).diff(l.lastMessage?.dateTime)
  );
};
