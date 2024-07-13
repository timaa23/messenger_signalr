import * as MessageActionCreators from "../actions/messages";
import * as AuthActionCreators from "../actions/auth";
import * as ConversationActionCreators from "../actions/conversations";

const actions = {
  ...MessageActionCreators,
  ...AuthActionCreators,
  ...ConversationActionCreators,
};

export default actions;
