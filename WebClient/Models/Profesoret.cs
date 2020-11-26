using System;
using System.Collections.Generic;

#nullable disable

namespace WebClient.Models
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

        public string FullName
        {
            get { return $"{Emri} {Mbiemri}"; }
        }

        public virtual ICollection<Lendet> Lendets { get; set; }
    }
}
