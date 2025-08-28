#nullable disable

using Church.Domain.Models;
using Church.Infrastructure.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Church.Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IUserDataService userDataService) : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public UserDataModel PersonalData { get; set; }

        [BindProperty]
        public ChangePasswordModel PasswordChange { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar o usuário com Id: '{userManager.GetUserId(User)}'.");
            }

            var hasPassword = await userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfile()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar o usuário com Id: '{userManager.GetUserId(User)}'.");
            }

            await userDataService.Set(user.Id, PersonalData);

            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = "Erro inesperado ao tentar salvar dados pessoais.";
                return RedirectToPage();
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Os dados de seu perfil foram atualizados com sucesso.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            ModelState.ClearValidationState(nameof(PersonalData));
            TryValidateModel(PasswordChange, nameof(PasswordChange));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Não foi possível encontrar o usuário com Id: '{userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, PasswordChange.CurrentPassword, PasswordChange.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = "Sua senha foi alterada com sucesso.";

            return RedirectToPage();
        }

        private async Task LoadAsync(IdentityUser user)
        {

            var aspNetUser = await userManager.GetUserAsync(User);
            if (aspNetUser is not null)
            {
                var userData = await userDataService.GetByAspNetUserId(aspNetUser.Id);
                if (userData is not null)
                {
                    PersonalData = userData;
                }
                return;
            }
            PersonalData = new UserDataModel();
        }

        #region ViewModels



        public class ChangePasswordModel
        {
            [Display(Name = "Senha Atual")]
            [Required(ErrorMessage = "A Senha atual é obrigatória.")]
            [DataType(DataType.Password)]
            public string CurrentPassword { get; set; } = string.Empty;

            [Display(Name = "Nova senha")]
            [Required(ErrorMessage = "A Nova senha é obrigatória.")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            [Display(Name = "Confirmação de Senha")]
            [Required(ErrorMessage = "A Confirmação de senha é obrigatória.")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "A confirmação não confere com a nova senha.")]
            public string PasswordConfirmation { get; set; } = string.Empty;

        }
        #endregion
    }
}
