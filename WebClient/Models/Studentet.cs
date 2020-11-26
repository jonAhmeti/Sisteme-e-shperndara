using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebClient.Models
{
    public partial class Studentet
    {
        public Studentet()
        {
            ProvimetStudenteves = new HashSet<ProvimetStudenteve>();
        }

        public int Id { get; set; }
        [Required]
        public string Emri { get; set; }
        [Required]
        public string Mbiemri { get; set; }

        public string FullName
        {
            get { return $"{Emri} {Mbiemri}"; }
        }

        public DateTime? DataLindjes { get; set; }
        public int? DrejtimiId { get; set; }
        public int? StatusiId { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }


        public virtual Drejtimet Drejtimi { get; set; }
        public virtual Statuset Statusi { get; set; }
        public virtual ICollection<ProvimetStudenteve> ProvimetStudenteves { get; set; }
    }
}
