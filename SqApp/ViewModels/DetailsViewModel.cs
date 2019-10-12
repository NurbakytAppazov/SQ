﻿using SqApp.DbData;
using System.Collections.Generic;

namespace SqApp.ViewModels
{
    public class DetailsViewModel
    {
        public Product Products {get;set;}
        public List<Product> Productes { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }

        public IEnumerable<ProductInfo> ProductInfos { get; set; }

        public List<Category> Categories { get; set; }

        public DetailsViewModel()
        {
            Productes = new List<Product>();
            Categories = new List<Category>();
        }
    }
}
