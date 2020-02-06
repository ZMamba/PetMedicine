using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Medicine.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
