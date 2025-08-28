using System.ComponentModel.DataAnnotations;

namespace Church.Domain.Models
{
    public class UserDataModel : BaseModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Sobrenome")]
        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string PostalCode { get; set; } = null!;

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Street { get; set; } = null!;

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O Número é obrigatório.")]
        public int Number { get; set; }

        [Display(Name = "Complemento Endereço")]
        public string Complement { get; set; } = null!;

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A Cidade é obrigatória.")]
        public string City { get; set; } = null!;

        [Phone]
        [Display(Name = "Número telefônico")]
        [Required(ErrorMessage = "o número telefônico é obrigatório.")]
        public string Phone { get; set; } = null!;
    }
}
