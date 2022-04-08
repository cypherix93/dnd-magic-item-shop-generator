using MagicItemShop.Web.DMPG;
using MagicItemShop.Web.DMPG.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MagicItemShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IList<MagicItem> MagicItems { get; set; }

        public IList<DMPG.Models.MagicItemShop> MagicItemShops { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            MagicItemShops = await Task.WhenAll(
                new DMPG.Models.MagicItemShop[10]
                    .Select(_ => MagicItemShopGenerator.GenerateMagicItemShop())
            );
        }
    }
}