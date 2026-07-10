import { useState, type FormEvent } from 'react';
import { Loading } from '../../components/Loading/Loading';
import { ErrorMessage } from '../../components/ErrorMessage/ErrorMessage';
import { Button } from '../../components/Button/Button';
import { Input } from '../../components/Input/Input';
import { useTransacoes } from '../../hooks/useTransacoes';
import { usePessoas } from '../../hooks/usePessoas';
import type { TransacaoTipo } from '../../types/transacao';
import styles from './TransacoesPage.module.css';

export function TransacoesPage() {
  const { transacoes, loading, error, salvando, erroSalvar, criarTransacao } = useTransacoes();
  const { pessoas } = usePessoas();

  const [descricao, setDescricao] = useState('');
  const [valor, setValor] = useState('');
  const [tipo, setTipo] = useState<TransacaoTipo>('Despesa');
  const [pessoaId, setPessoaId] = useState('');

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();

    if (!descricao || !valor || !pessoaId) return;

    const sucesso = await criarTransacao({ descricao, valor: Number(valor), tipo, pessoaId });

    if (sucesso) {
      setDescricao('');
      setValor('');
      setPessoaId('');
    }
  };

  return (
    <div>
      <h2>Transações</h2>

      <form onSubmit={handleSubmit} className={styles.form}>
        <Input label="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} required />
        <Input label="Valor" type="number" step="0.01" value={valor} onChange={(e) => setValor(e.target.value)} required />

        <label className={styles.selectLabel}>
          Tipo
          <select value={tipo} onChange={(e) => setTipo(e.target.value as TransacaoTipo)} className={styles.select}>
            <option value="Despesa">Despesa</option>
            <option value="Receita">Receita</option>
          </select>
        </label>

        <label className={styles.selectLabel}>
          Pessoa
          <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} className={styles.select} required>
            <option value="">Selecione...</option>
            {pessoas.map((pessoa) => (
              <option key={pessoa.id} value={pessoa.id}>
                {pessoa.nome}
              </option>
            ))}
          </select>
        </label>

        <Button type="submit" disabled={salvando}>
          {salvando ? 'Salvando...' : 'Cadastrar transação'}
        </Button>
        {erroSalvar && <ErrorMessage message={erroSalvar} />}
      </form>

      {loading && <Loading />}
      {error && <ErrorMessage message={error} />}

      {!loading && !error && (
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
      )}
    </div>
  );
}