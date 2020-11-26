using System;
using System.Collections.Generic;

#nullable disable

namespace WebClient.Models
{
    public partial class Drejtimet
    {
        public Drejtimet()
        {
            Lendets = new HashSet<Lendet>();
            Studentets = new HashSet<Studentet>();
        }

        public int Id { get; set; }
        public string Emri { get; set; }
        public string Komenti { get; set; }

        public virtual ICollection<Lendet> Lendets { get; set; }
        public virtual ICollection<Studentet> Studentets { get; set; }
    }
}
