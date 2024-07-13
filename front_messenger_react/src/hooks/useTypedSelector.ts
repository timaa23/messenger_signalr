import { useSelector } from "react-redux";
import { TypedUseSelectorHook } from "react-redux";
import { rootState } from "./../store/index";

export const useTypedSelector: TypedUseSelectorHook<rootState> = useSelector;
