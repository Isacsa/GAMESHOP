using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class PerfilCategoria
    {
        [Key]
        public int Id { get; set; }

        public int perfilId { get; set; }
        public Perfil? perfil { get; set; }

        public int categoriaId { get; set; }
       public Categoria? Categoria { get; set; }

    }
}