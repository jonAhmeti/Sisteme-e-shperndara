using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class Studentet
    {
        public Studentet()
        {
            ProvimetStudenteves = new HashSet<ProvimetStudenteve>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }
        public DateTime? DataLindjes { get; set; }
        public int? DrejtimiId { get; set; }
        public int? StatusiId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual Drejtimet Drejtimi { get; set; }
        public virtual Statuset Statusi { get; set; }
        public virtual ICollection<ProvimetStudenteve> ProvimetStudenteves { get; set; }
    }
}
