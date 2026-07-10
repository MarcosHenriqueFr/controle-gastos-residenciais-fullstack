export interface PessoaResumo {
  pessoaId: string;
  nome: string;
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface ResumoTotais {
  totalReceitas: number;
  totalDespesas: number;
  saldo: number;
}

export interface ResumoGeral {
  pessoas: PessoaResumo[];
  totalGeral: ResumoTotais;
}