using System;
using System.Collections.Generic;

namespace TomasosPizzeria.Models
{
    public partial class Korg
    {
        public int KorgId { get; set; }
        public int? Matrattid { get; set; }
        public string Matrattnamn { get; set; }
        public int? Pris { get; set; }
        public int KundId { get; set; }
    }
}
