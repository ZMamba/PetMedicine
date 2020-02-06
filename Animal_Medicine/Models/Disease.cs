using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Animal_Medicine.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name ="Теги")]
        public string Tags { get; set; }

        public int PetTypeId { get; set; }
        public PetType PetType { get; set; }

        public string ImageName { get; set; }
    }
}
