import * as MessageActionCreators from "../messages/actions";
import * as AuthActionCreators from "../auth/actions";

const actions = {
  ...MessageActionCreators,
  ...AuthActionCreators,
};

export default actions;
