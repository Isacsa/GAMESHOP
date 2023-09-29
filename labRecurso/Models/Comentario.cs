using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        public int JogoId { get; set; }
        public Jogos? Jogo { get; set; }

        [Required(ErrorMessage = "É necessário um Autor")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É necessário um comentário")]
        public string Message { get; set; }
    }
}
