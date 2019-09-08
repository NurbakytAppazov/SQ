using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqApp.DbData
{
    public class Pod_Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string UrlIcon { get; set; }
    }
}
