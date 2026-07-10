export type TransacaoTipo = 'Despesa' | 'Receita';

export interface Transacao {
  id: string;
  descricao: string;
  valor: number;
  tipo: TransacaoTipo;
  pessoaId: string;
}

export interface CriarTransacaoRequest {
  descricao: string;
  valor: number;
  tipo: TransacaoTipo;
  pessoaId: string;
}