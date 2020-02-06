using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Animal_Medicine.Models;
using System.ComponentModel.DataAnnotations;

namespace Animal_Medicine.ViewModels
{
    public class CreateDiseaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name ="Симптомы")]
        public string Tags { get; set; }

        [Display(Name = "Тип животного")]
        public int PetTypeId { get; set; }

        public List<PetType> PetTypes { get; set; }
    }
}
