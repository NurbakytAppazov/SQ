using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqApp.DbData
{
    public class InProductInfo
    {
        public int Id { get; set; }

        public string InfoName { get; set; }

        public string InfoValue { get; set; }

        public int ProductInfoId { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }
}
