using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class Lendet
    {
        public Lendet()
        {
            Provimets = new HashSet<Provimet>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }
        public byte? Semestri { get; set; }
        public int? DrejtimiId { get; set; }
        public int? ProfesoriId { get; set; }

        public virtual Drejtimet Drejtimi { get; set; }
        public virtual Profesoret Profesori { get; set; }
        public virtual ICollection<Provimet> Provimets { get; set; }
    }
}
