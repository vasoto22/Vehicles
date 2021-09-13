﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

    namespace Vehicles.API.Data.Entities
    {
        public class DocumentType
        {
            public int Id { get; set; }

            [Display(Name = "Tipo de documento")]
            [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
            [Required(ErrorMessage = "El campo {0} es obligatorio.")]

            [JsonIgnore]
            public string Description { get; set; }

        }
    }

