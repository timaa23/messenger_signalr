import * as MessageActionCreators from "../messages/actions";
import * as AuthActionCreators from "../auth/actions";
import * as ConversationActionCreators from "../conversations/actions";

const actions = {
  ...MessageActionCreators,
  ...AuthActionCreators,
  ...ConversationActionCreators,
};

export default actions;
