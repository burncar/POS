using Microsoft.AspNetCore.Mvc.Rendering;

namespace POS.Web.Models
{
    public class EProductListSelectListItem: SelectListItem
    {
        public int? ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
    }
}
