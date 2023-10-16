using System.ComponentModel.DataAnnotations;

namespace uyg01.Models
{
    public class Product
    {
        public int Id { get; set; }




        [Required(ErrorMessage = "Ürün Adı Giriniz")]

        public string Name { get; set; }



        [Required(ErrorMessage = "Ürün Açıklaması Giriniz")]
        public string Description { get; set; }

        public bool Status { get; set; }

    }
}
