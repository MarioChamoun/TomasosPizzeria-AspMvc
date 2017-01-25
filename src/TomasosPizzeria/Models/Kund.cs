using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Models
{
    public partial class Kund
    {
        public Kund()
        {
            Bestallning = new HashSet<Bestallning>();
        }

        public int KundId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett namn")]
        public string Namn { get; set; }
        [Required(ErrorMessage = "Du måste fylla i gatuadress")]
        public string Gatuadress { get; set; }
        [Required(ErrorMessage = "Du måste fylla i postnummer")]
        public string Postnr { get; set; }
        [Required(ErrorMessage = "Du måste fylla i postort")]
        public string Postort { get; set; }
        [Required(ErrorMessage = "Du måste fylla i email")]
        [EmailAddress(ErrorMessage = "Fyll i en korrekt email adress")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Du måste fylla i telefon")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Du måste fylla i användarnamn")]
        public string AnvandarNamn { get; set; }
        [Required(ErrorMessage = "Du måste fylla i lösenord")]
        public string Losenord { get; set; }


        public virtual ICollection<Bestallning> Bestallning { get; set; }
    }
}
