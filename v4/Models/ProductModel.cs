using System.ComponentModel.DataAnnotations;

namespace UrunSatisApp.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
    }

    public class Cart
    {
        public string Name { get; set; }
        public int PaidPrice { get; set; }
        public int Quantity { get; set; }
    }
}
