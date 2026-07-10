import { useFetch } from './useFetch';
import { resumoService } from '../services/resumoService';

export function useResumo() {
  return useFetch(() => resumoService.obter(), []);
}