﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spice.Models.ViewModel
{
    public class SubcategoryAndCategoryViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public SubCategory Subcategory { get; set; }
        public List<string> SubCategoryList { get; set; }
        public string StatusMessage { get; set; }   
    }
}
