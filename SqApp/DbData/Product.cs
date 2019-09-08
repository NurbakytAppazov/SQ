using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqApp.DbData
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FirstInfo { get; set; }

        public int? Price { get; set; }

        public int? MinPay { get; set; }

        public string PublicImage { get; set; }



        public int CategoryId { get; set; }

        public int Pod_CategoryId { get; set; }
        public Pod_Category Pod_Category { get; set; }
    }
}
