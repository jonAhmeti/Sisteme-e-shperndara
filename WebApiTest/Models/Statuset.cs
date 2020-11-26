using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiTest.Models
{
    public partial class Statuset
    {
        public Statuset()
        {
            Studentets = new HashSet<Studentet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Studentet> Studentets { get; set; }
    }
}
