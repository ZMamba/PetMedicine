using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animal_Medicine.Models;

namespace Animal_Medicine.ViewModels
{
    public class MainViewModel
    {
        //public List<Disease> Diseases { get; set; }
        public PaginatedList<Disease> Diseases { get; set; }
        public List<PetType> PetTypes { get; set; }
    }
}
