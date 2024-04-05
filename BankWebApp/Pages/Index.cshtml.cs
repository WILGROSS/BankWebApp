using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ViewModels;

namespace BankWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILandingPageService _landingPageService;
        private readonly ILogger<IndexModel> _logger;

        public LandingPageViewModel _landingPageViewModel;
        public IndexModel(ILogger<IndexModel> logger, ILandingPageService landingPageService)
        {
            _logger = logger;
            _landingPageService = landingPageService;
        }

        public void OnGet()
        {
            _landingPageViewModel = _landingPageService.GetInfo();
        }
    }
}
