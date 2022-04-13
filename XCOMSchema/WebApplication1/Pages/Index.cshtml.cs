using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XCOM.Schema.Core.Infrastructure.IOC;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ITest _test;
        private readonly ITest _test1 = XMIOC.Resolve<ITest>();

        public IndexModel(ILogger<IndexModel> logger, ITest test)
        {
            _logger = logger;
            _test = test;
        }

        public void OnGet()
        {

        }
    }
}