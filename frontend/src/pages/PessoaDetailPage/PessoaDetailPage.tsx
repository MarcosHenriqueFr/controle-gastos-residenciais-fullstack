import { useParams } from 'react-router-dom';
import { Loading } from '../../components/Loading/Loading';
import { ErrorMessage } from '../../components/ErrorMessage/ErrorMessage';
import { usePessoaDetalhe } from '../../hooks/usePessoaDetalhe';
import styles from './PessoaDetailPage.module.css';

export function PessoaDetailPage() {
  const { id } = useParams<{ id: string }>();
  const { pessoa, transacoes, loading, error } = usePessoaDetalhe(id ?? '');

  if (loading) return <Loading />;
  if (error) return <ErrorMessage message={error} />;
  if (!pessoa) return null;

  return (
    <div>
      <h2>{pessoa.nome}</h2>
      <p>{pessoa.idade} anos</p>

      <h3>Transações</h3>
      {transacoes.length === 0 && <p>Nenhuma transação cadastrada.</p>}

      <ul className={styles.list}>
        {transacoes.map((transacao) => (
          <li key={transacao.id} className={styles.item}>
            <span>{transacao.descricao}</span>
            <span className={transacao.tipo === 'Receita' ? styles.receita : styles.despesa}>
              {transacao.tipo === 'Receita' ? '+' : '-'} R$ {transacao.valor.toFixed(2)}
            </span>
          </li>
        ))}
      </ul>
    </div>
  );
}