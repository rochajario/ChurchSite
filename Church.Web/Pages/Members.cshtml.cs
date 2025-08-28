using Church.Domain.Entities;
using Church.Infrastructure.Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Church.Web.Pages
{
    public class MembersModel(IUserDataRepository userDataRepository) : PageModel
    {

        private List<UserData> _users = Enumerable.Empty<UserData>().ToList();

        [BindProperty]
        public List<UserData> MembersToValidate { get { return [.. _users.Where(x => !x.WereAccepted)]; } }

        [BindProperty]
        public List<UserData> MembersToPromote { get { return [.. _users.Where(x => !x.IsAdmin)]; } }

        public async Task OnGetAsync()
        {
            _users = await userDataRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAcceptAsync(long id)
        {
            var user = await userDataRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            user.WereAccepted = true;
            await userDataRepository.UpdateAsync(user);

            // redireciona para recarregar a lista atualizada
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRejectAsync(long id)
        {
            var user = await userDataRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            await userDataRepository.DeleteAsync(id);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostPromoteAsync(long id)
        {
            var user = await userDataRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            user.IsAdmin = true;
            await userDataRepository.UpdateAsync(user);

            return RedirectToPage();
        }
    }
}

