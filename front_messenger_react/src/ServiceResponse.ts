export default interface ServiceResponse<T> {
  message: string;
  isSuccess: boolean;
  payload: T;
}
