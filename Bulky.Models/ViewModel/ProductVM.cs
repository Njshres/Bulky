using System.Web.Mvc;

namespace Bulky.Models.ViewModel

{
    public class ProductVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public List<SelectListItem>? CategoryList {get; set; }
    }
}
