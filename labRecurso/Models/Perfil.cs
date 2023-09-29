using labRecurso.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class Perfil
    {
        [Key]
        public int Id { get; set; }

        public string utilizadorId { get; set; }
        [Required]
        public IdentityUser utilizador { get; set; }

        public float saldo { get; set; } = 0;

        public virtual ICollection<PerfilCategoria>? categoriasFavoritas { get; set; } = new List<PerfilCategoria>();

        public virtual ICollection<PerfilJogos>? jogosComprados { get; set; } = new List<PerfilJogos>();
    }
}