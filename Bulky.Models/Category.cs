using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {

        public int Id { get; set; }

        public string? Name { get; set; }

        public int? DispalyOrder { get; set; }

    }
}
