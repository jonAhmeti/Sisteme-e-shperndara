using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class Provimet
    {
        public Provimet()
        {
            ProvimetStudenteves = new HashSet<ProvimetStudenteve>();
        }

        public int Id { get; set; }
        public int? LendaId { get; set; }
        public DateTime Data { get; set; }

        public virtual Lendet Lenda { get; set; }
        public virtual ICollection<ProvimetStudenteve> ProvimetStudenteves { get; set; }
    }
}
