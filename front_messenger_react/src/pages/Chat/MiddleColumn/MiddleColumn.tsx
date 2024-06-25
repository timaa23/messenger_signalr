import styles from "./index.module.scss";

const MiddleColumn = () => {
  return (
    <>
      <main className={styles.middle_column}>
        <div className={styles.middle_column_main}>
          <header className={styles.middle_column_main_header}>User</header>
          <div className={styles.middle_column_main_list}></div>
        </div>
      </main>
    </>
  );
};

export default MiddleColumn;
