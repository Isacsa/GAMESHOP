using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class PerfilJogos
    {
        [Key]
        public int Id { get; set; }

        public int perfilId { get; set; }
        public Perfil? perfil { get; set; }

        public int jogoId { get; set; }
        public Jogos? jogo { get; set; }
    }
}
