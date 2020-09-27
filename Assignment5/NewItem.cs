﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment5
{
    public class NewItem
    {
        [Range(1, 99, ErrorMessage = "Incorrect Value! Values between {1} and {2} allowed.")]
        public int Level { get; set; }

        [EnumDataType(typeof(ItemType), ErrorMessage = "Invalid ItemType")]
        [Range(0, 2)]
        public ItemType Type { get; set; }

        [DateValidation]
        public DateTime CreationDate { get; set; }
    }
}
