import { Routes, Route } from 'react-router-dom';
import { Layout } from '../components/Layout/Layout';
import { PessoasPage } from '../pages/PessoasPage/PessoasPage';
import { PessoaDetailPage } from '../pages/PessoaDetailPage/PessoaDetailPage';
import { TransacoesPage } from '../pages/TransacoesPage/TransacoesPage';
import { ResumoPage } from '../pages/ResumoPage/ResumoPage';

export function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<PessoasPage />} />
        <Route path="pessoa/:id" element={<PessoaDetailPage />} />
        <Route path="transacoes" element={<TransacoesPage />} />
        <Route path="resumo" element={<ResumoPage />} />
      </Route>
    </Routes>
  );
}