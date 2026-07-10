import { useState, type FormEvent } from 'react';
import { Link } from 'react-router-dom';
import { Loading } from '../../components/Loading/Loading';
import { ErrorMessage } from '../../components/ErrorMessage/ErrorMessage';
import { Button } from '../../components/Button/Button';
import { Input } from '../../components/Input/Input';
import { usePessoas } from '../../hooks/usePessoas';
import styles from './PessoasPage.module.css';

export function PessoasPage() {
  const { pessoas, loading, error, salvando, erroSalvar, criarPessoa, removerPessoa } = usePessoas();
  const [nome, setNome] = useState('');
  const [idade, setIdade] = useState('');

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();

    if (!nome || !idade) return;

    await criarPessoa({ nome, idade: Number(idade) });
    setNome('');
    setIdade('');
  };

  return (
    <div>
      <h2>Pessoas</h2>

      <form onSubmit={handleSubmit} className={styles.form}>
        <Input label="Nome" value={nome} onChange={(e) => setNome(e.target.value)} required />
        <Input label="Idade" type="number" value={idade} onChange={(e) => setIdade(e.target.value)} required />
        <Button type="submit" disabled={salvando}>
          {salvando ? 'Salvando...' : 'Cadastrar pessoa'}
        </Button>
        {erroSalvar && <ErrorMessage message={erroSalvar} />}
      </form>

      {loading && <Loading />}
      {error && <ErrorMessage message={error} />}

      {!loading && !error && (
        <ul className={styles.list}>
          {pessoas.map((pessoa) => (
            <li key={pessoa.id} className={styles.item}>
              <Link to={`/pessoa/${pessoa.id}`}>
                {pessoa.nome} ({pessoa.idade} anos)
              </Link>
              <Button variant="danger" onClick={() => removerPessoa(pessoa.id)}>
                Remover
              </Button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}