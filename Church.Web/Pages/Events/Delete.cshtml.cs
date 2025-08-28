using Church.Domain.Entities;
using Church.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Church.Web.Pages.Events
{
    public class DeleteModel(ApplicationContext context) : PageModel
    {

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEntity = await context.Events.FindAsync(id);
            if (eventEntity != null)
            {
                Event = eventEntity;
                context.Events.Remove(Event);
                await context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
