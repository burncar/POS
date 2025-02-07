using Microsoft.AspNetCore.Mvc.Rendering;

namespace POS.Web.Models
{
    public class ExtendedSelectListItem : SelectListItem
    {
        public int? ProductBrandId { get; set; }
        public int? ProductTypeId { get; set; }
    }
}
