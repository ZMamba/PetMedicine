using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Medicine.Models
{
    public class PetType
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Disease> Disease { get; set; }
    }
}
