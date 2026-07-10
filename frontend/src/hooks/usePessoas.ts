import { useState } from 'react';
import { useFetch } from './useFetch';
import { pessoaService } from '../services/pessoaService';
import type { CriarPessoaRequest } from '../types/pessoa';

export function usePessoas() {
  const { data: pessoas, loading, error, refetch } = useFetch(() => pessoaService.listar(), []);
  const [salvando, setSalvando] = useState(false);
  const [erroSalvar, setErroSalvar] = useState<string | null>(null);

  const criarPessoa = async (request: CriarPessoaRequest) => {
    setSalvando(true);
    setErroSalvar(null);

    try {
      await pessoaService.criar(request);
      refetch();
    } catch (err) {
      setErroSalvar((err as Error).message);
    } finally {
      setSalvando(false);
    }
  };

  const removerPessoa = async (id: string) => {
    try {
      await pessoaService.remover(id);
      refetch();
    } catch (err) {
      setErroSalvar((err as Error).message);
    }
  };

  return { pessoas: pessoas ?? [], loading, error, salvando, erroSalvar, criarPessoa, removerPessoa };
}