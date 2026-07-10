import type { ButtonHTMLAttributes } from 'react';
import styles from './Button.module.css';

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: 'primary' | 'danger';
}

export function Button({ variant = 'primary', className, ...rest }: ButtonProps) {
  const variantClass = variant === 'danger' ? styles.danger : styles.primary;

  return <button className={`${styles.button} ${variantClass} ${className ?? ''}`} {...rest} />;
}