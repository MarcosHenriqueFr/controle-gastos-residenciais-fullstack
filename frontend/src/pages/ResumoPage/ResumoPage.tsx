import { Loading } from '../../components/Loading/Loading';
import { ErrorMessage } from '../../components/ErrorMessage/ErrorMessage';
import { useResumo } from '../../hooks/useResumo';
import styles from './ResumoPage.module.css';

export function ResumoPage() {
  const { data: resumo, loading, error } = useResumo();

  if (loading) return <Loading />;
  if (error) return <ErrorMessage message={error} />;
  if (!resumo) return null;

  return (
    <div>
      <h2>Resumo financeiro</h2>

      <table className={styles.table}>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {resumo.pessoas.map((pessoa) => (
            <tr key={pessoa.pessoaId}>
              <td>{pessoa.nome}</td>
              <td>R$ {pessoa.totalReceitas.toFixed(2)}</td>
              <td>R$ {pessoa.totalDespesas.toFixed(2)}</td>
              <td className={pessoa.saldo >= 0 ? styles.positivo : styles.negativo}>
                R$ {pessoa.saldo.toFixed(2)}
              </td>
            </tr>
          ))}
        </tbody>
        <tfoot>
          <tr>
            <td><strong>Total geral</strong></td>
            <td>R$ {resumo.totalGeral.totalReceitas.toFixed(2)}</td>
            <td>R$ {resumo.totalGeral.totalDespesas.toFixed(2)}</td>
            <td className={resumo.totalGeral.saldo >= 0 ? styles.positivo : styles.negativo}>
              R$ {resumo.totalGeral.saldo.toFixed(2)}
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  );
}