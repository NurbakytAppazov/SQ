using SqApp.DbData;
using System.Collections.Generic;

namespace SqApp.ViewModels
{
    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<LateView> LateViews { get; set; }
        public List<Category> Categories { get; set; }

        public ShopViewModel() {
            Products = new List<Product>();
            Categories = new List<Category>();
        }
    }
}
