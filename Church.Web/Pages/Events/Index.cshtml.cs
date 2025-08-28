using Church.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly Church.Infrastructure.Data.ApplicationContext _context;

        public IndexModel(Church.Infrastructure.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Event> Event { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Event = await _context.Events.ToListAsync();
        }
    }
}
