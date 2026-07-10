import { NavLink, Outlet } from 'react-router-dom';
import styles from './Layout.module.css';

export function Layout() {
  return (
    <div className={styles.container}>
      <header className={styles.header}>
        <h1 className={styles.title}>Gastos Residenciais</h1>
        <nav className={styles.nav}>
          <NavLink to="/" end className={({ isActive }) => (isActive ? styles.activeLink : styles.link)}>
            Pessoas
          </NavLink>
          <NavLink to="/transacoes" className={({ isActive }) => (isActive ? styles.activeLink : styles.link)}>
            Transações
          </NavLink>
          <NavLink to="/resumo" className={({ isActive }) => (isActive ? styles.activeLink : styles.link)}>
            Resumo
          </NavLink>
        </nav>
      </header>

      <main className={styles.content}>
        <Outlet />
      </main>
    </div>
  );
}