import { ButtonHTMLAttributes, forwardRef } from "react";
import styles from "./index.module.scss";

interface AuthButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  label: string;
}

const AuthButton = forwardRef<HTMLButtonElement, AuthButtonProps>(
  ({ label, ...props }, ref) => {
    return (
      <>
        <button ref={ref} className={styles.button} {...props}>
          <span>{label}</span>
        </button>
      </>
    );
  }
);

export default AuthButton;
