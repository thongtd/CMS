﻿using System.ComponentModel.DataAnnotations;
using Framework.Core.Extension;

namespace Framework.Models
{
    public class TagRequest
    {
        [Required(ErrorMessage = Constants.Validation.Required)]
        public string ObjectName { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string ObjectProperty { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public string ObjectValue { get; set; }

        [Required(ErrorMessage = Constants.Validation.Required)]
        public int TagCategory { get; set; }
    }
}