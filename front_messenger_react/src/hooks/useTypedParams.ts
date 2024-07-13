import moment from "moment";
import { useParams } from "react-router-dom";

export function useTypedParams(key: string): null | string;
export function useTypedParams(key: string, type: "string"): null | string;
export function useTypedParams(key: string, type: "number"): null | number;
export function useTypedParams(key: string, type: "date"): null | Date;
export function useTypedParams(
  key: string,
  type?: "string" | "number" | "date"
): null | number | string | Date {
  const params = useParams();
  const value = params[key];

  if (!value) return null;

  switch (type) {
    case "number":
      return Number(value);
    case "date":
      return moment(value).toDate();
    default:
      return value;
  }
}
