using Church.Domain.Entities;
using Church.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Pages.Events
{
    public class DetailsModel(ApplicationContext context) : PageModel
    {
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEntity = await context.Events.FirstOrDefaultAsync(m => m.Id == id);

            if (eventEntity is not null)
            {
                Event = eventEntity;

                return Page();
            }

            return NotFound();
        }
    }
}
