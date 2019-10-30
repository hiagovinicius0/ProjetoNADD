using ProjetoNADD.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjetoNADD.Models
{
    public class Questao
    {
        [Key]
        [Display(Name = "Id")]
        public int Id_Questao { get; set; }
        [Display(Name = "Número")]
        public int Id_Numero { get; set; }
        [Display(Name = "Avaliação")]
        public int Id_Avaliacao { get; set; }
        public Avaliacao Avaliacao { get; set; }
        [Display(Name = "Contextualização")]
        public bool Contextualizacao_Questao { get; set; }
        [Display(Name = "Clareza")]
        public bool Clareza_Questao { get; set; }
        [Display(Name = "Complexidade")]
        public int? ComplexidadeID { get; set; }
        public Complexidade Complexidade { get; set; }
        [Display(Name = "Tipo")]
        public int? TipoID { get; set; }
        public TipoQuestao TipoQuestao { get; set; }
        [Display(Name = "Observações")]
        public string Observacoes_Questao { get; set; }
        public void SicronizarAvaliacoes(int id, ProjetoNADDContext _context)
        {
            var avaliacao = _context.Questao.Where(q => q.Id_Avaliacao == id).ToList();
            if (avaliacao.Count() == 0)
            {
                return;
            }
            else
            {
                int marcar = 0;
                int discursiva = 0;
                bool contextualizacao = false;
                bool clareza = false;
                foreach (var item in avaliacao)
                {
                    if (item.TipoID == 1)
                    {
                        marcar++;
                    }
                    else if (item.TipoID == 2)
                    {
                        discursiva++;
                    }
                    if (item.Contextualizacao_Questao == true)
                    {
                        contextualizacao = true;
                    }
                    if (item.Clareza_Questao == true)
                    {
                        clareza = true;
                    }
                }
                string QuestoesMeed = "";
                if (marcar > 0 && discursiva > 0)
                {
                    QuestoesMeed = "Apresenta Questões de Múltipla Escolha e Discursivas";
                }
                else if (marcar == 0 && discursiva > 0)
                {
                    QuestoesMeed = "Apresenta Somente Questões Discursivas";
                }
                else if (marcar > 0 && discursiva == 0)
                {
                    QuestoesMeed = "Apresenta Somente Questões de Múltipla Escolha";
                }
                int quantQuestoes = avaliacao.Count();
                Avaliacao avaliacaoUpdate = _context.Avaliacao.Where(d => d.Id_Avaliacao == id).FirstOrDefault<Avaliacao>();
                avaliacaoUpdate.QuestoesMEeD_Avaliacao = QuestoesMeed;
                avaliacaoUpdate.NumeroQuestoes_Avaliacao = quantQuestoes;
                avaliacaoUpdate.Contextualidade_Avaliacao = contextualizacao;
                avaliacaoUpdate.Clareza_Avaliacao = clareza;
                _context.Update(avaliacaoUpdate);
                _context.SaveChanges();
            }
        }
    }
}
