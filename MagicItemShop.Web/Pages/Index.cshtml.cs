using MagicItemShop.Core.App.MagicItemShop;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagicItemShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IList<Core.App.MagicItemShop.Models.MagicItemShop> MagicItemShops { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            MagicItemShops = new Core.App.MagicItemShop.Models.MagicItemShop[10]
                .Select(_ => MagicItemShopGenerator.GenerateMagicItemShop())
                .ToList();
        }
    }
}