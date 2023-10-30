using System.ComponentModel.DataAnnotations;

namespace uyg02.ViewModels
{
    public class ProductModel

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün Adı Giriniz!")]
        public string Name { get; set; }




        [Required(ErrorMessage = "Ürün Açıklaması Giriniz!")]
        public string Description { get; set; }



        public bool Status { get; set; }



        [Required(ErrorMessage = "Ürün Fiyatı Giriniz!")]
        public decimal Price { get; set; }
    }
}
