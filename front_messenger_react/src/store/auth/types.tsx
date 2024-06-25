export interface ILoginCredentials {
  email: string;
  password: string;
}

export interface IRegisterCredentials {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface IUser {
  id: number;
  userName: string;
  image?: string;
  roles: string[];
}

export interface IAuthState {
  isAuth: boolean;
  user: IUser | null;
}

export enum AuthActionTypes {
  LOGIN_USER = "AUTH_LOGIN_USER",
  REGISTER_USER = "AUTH_REGISTER_USER",
  LOGOUT_USER = "AUTH_LOGOUT_USER",
}

export interface LoginUserAction {
  type: AuthActionTypes.LOGIN_USER;
  payload: IAuthState;
}

export interface RegisterUserAction {
  type: AuthActionTypes.REGISTER_USER;
}

export interface LogoutUserAction {
  type: AuthActionTypes.LOGOUT_USER;
}

export type AuthActions = LoginUserAction | RegisterUserAction | LogoutUserAction;
