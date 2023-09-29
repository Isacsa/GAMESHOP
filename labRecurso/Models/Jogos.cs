
using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class Jogos
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório o jogo ter um Nome")]
        public string Nome { get; set; }



        [RegularExpression(@"^.+\.([jJ][pP][gG]|[pP][nN][gG])$", ErrorMessage = "Só jpg e png files")]
        public string? Foto { get; set; }

        [Required(ErrorMessage = "É obrigatório o jogo ter um preço")]
        public float Preco { get; set; }
        public int plataformaId { get; set; }
        public Plataforma? plataforma { get; set; }
        public int Pontuacao { get; set; } = 0;
        public int categoriaId { get; set; }
        public Categoria? categoria { get; set; }


        public ICollection<Comentario> Comentarios = new List<Comentario>();

    }
}
