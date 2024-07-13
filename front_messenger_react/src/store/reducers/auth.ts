import { AuthActionTypes, AuthActions, IAuthState } from "../types/auth";

const initState: IAuthState = {
  isAuth: false,
  user: {
    id: 0,
    userName: "",
    roles: [],
  },
};

export const AuthReducer = (state = initState, action: AuthActions): IAuthState => {
  switch (action.type) {
    case AuthActionTypes.LOGIN_USER: {
      return {
        ...state,
        ...action.payload,
      };
    }
    case AuthActionTypes.REGISTER_USER: {
      return {
        ...state,
      };
    }
    case AuthActionTypes.LOGOUT_USER: {
      return {
        ...state,
        isAuth: false,
        user: null,
      };
    }
    default:
      return state;
  }
};
