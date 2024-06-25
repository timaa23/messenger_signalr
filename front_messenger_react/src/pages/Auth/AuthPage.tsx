import { useActions } from "../../hooks/useActions";
import { ILoginCredentials } from "../../store/auth/types";
import styles from "./index.module.scss";
import { useFormik } from "formik";
import * as Yup from "yup";
import AuthInput from "../../components/common/inputs/AuthInput/AuthInput";
import AuthButton from "../../components/common/buttons/AuthButton";

const AuthPage = () => {
  const { Login } = useActions();

  const onSubmitHandler = async (model: ILoginCredentials) => {
    console.log(model);

    try {
      await Login(model);
    } catch (error) {
      console.error("Щось пішло не так, ", error);
    }
  };

  //Formik
  const modelInitValues: ILoginCredentials = {
    email: "",
    password: "",
  };

  const loginSchema = Yup.object().shape({
    email: Yup.string().email("*Doesn't seem like email").required("*Required"),
    password: Yup.string().required("*Required"),
  });

  const formik = useFormik<ILoginCredentials>({
    initialValues: modelInitValues,
    validationSchema: loginSchema,
    onSubmit: onSubmitHandler,
  });

  const { values, errors, touched, handleSubmit, handleChange, handleBlur } = formik;

  return (
    <>
      <div className={styles.auth_page}>
        <div className={styles.auth_page_main}>
          <h1>Messenger</h1>
          <p className={styles.note}>Please login in your account to continue.</p>
          <form className={styles.form} onSubmit={handleSubmit}>
            {/* email field  */}
            <AuthInput
              id="email"
              type="email"
              inputMode="email"
              label="Email"
              value={values.email}
              onChange={handleChange}
              onBlur={handleBlur}
              errors={errors.email && touched.email}
            />

            {/* password field  */}
            <AuthInput
              id="password"
              type="password"
              inputMode="text"
              label="Password"
              value={values.password}
              onChange={handleChange}
              onBlur={handleBlur}
              errors={errors.password && touched.password}
            />

            {/* button submit  */}
            <AuthButton label="Next" type="submit" />
          </form>
        </div>
      </div>
    </>
  );
};

export default AuthPage;
