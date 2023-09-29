using System.ComponentModel.DataAnnotations;

namespace labRecurso.Models
{
    public class Plataforma
    {
        [Key]
       public int Id { get; set; }
        [Required(ErrorMessage ="Mete um nome correto:")]
        public string Name { get; set; }

    }
}
