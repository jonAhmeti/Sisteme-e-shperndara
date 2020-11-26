using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class Profesoret
    {
        public Profesoret()
        {
            Lendets = new HashSet<Lendet>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Mbiemri { get; set; }

        public virtual ICollection<Lendet> Lendets { get; set; }
    }
}
