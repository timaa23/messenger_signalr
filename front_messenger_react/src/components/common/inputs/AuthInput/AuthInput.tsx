import { InputHTMLAttributes, forwardRef } from "react";
import styles from "./index.module.scss";
import classNames from "classnames";

interface AuthInputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  errors: string | boolean | undefined;
}

const AuthInput = forwardRef<HTMLInputElement, AuthInputProps>(
  ({ label, placeholder, errors, ...props }, ref) => {
    return (
      <>
        <div className={styles.field}>
          <input
            ref={ref}
            placeholder=""
            className={classNames({
              [styles.error]: errors,
            })}
            {...props}
          />
          <label htmlFor={props.id}>{label}</label>
        </div>
      </>
    );
  }
);

export default AuthInput;
