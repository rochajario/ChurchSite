using Church.Domain.Entities;
using Church.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Pages
{
    public class IndexModel(ApplicationContext applicationContext, ILogger<IndexModel> logger) : PageModel
    {
        [BindProperty]
        public IEnumerable<Event> Events { get; internal set; } = Enumerable.Empty<Event>();

        public async Task OnGetAsync()
        {
            var week = DateTime.Now.AddDays(7);

            Events = await applicationContext.Events
                .Where(x => x.Date <= week)
                .ToListAsync();
        }
    }
}
