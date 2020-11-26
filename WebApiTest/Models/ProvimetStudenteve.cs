using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class ProvimetStudenteve
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ProvimId { get; set; }
        public int Piket { get; set; }

        public virtual Provimet Provim { get; set; }
        public virtual Studentet Student { get; set; }
    }
}
