using ClientProject.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ClientProject.Models
{
    public class ClientModel
    {
        private const string _MAX = "Maximum {0} characters";
        private const string _MIN = "Field {0} needs at least {1} characters";

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string Name { get; set; }

        /// <summary>
        /// Cpf is required and unique
        /// </summary>
        [Required]
        [MaxLength(11, ErrorMessage = _MAX)]
        [MinLength(11, ErrorMessage = _MIN)]
        [CustomValidationCPF(ErrorMessage = "Invalid CPF")]
        [Index(IsUnique = true)]
        public string Cpf { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = _MAX)]
        [CustomValidationEmail(ErrorMessage = "Invalid E-mail")]
        public string Email { get; set; }

        [Required]
        public int MaritalStatus { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string Street { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string Number { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string City { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string State { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = _MAX)]
        [MinLength(2, ErrorMessage = _MIN)]
        public string Country { get; set; }

        [NotMapped]
        public IEnumerable<string> PhoneNumbers { get; set; }

        public string Phones
        {
            get
            {
                return string.Join("|", PhoneNumbers);
            }
            set
            {
                PhoneNumbers = value.Split('|').ToList();
            }
        }
    }
}