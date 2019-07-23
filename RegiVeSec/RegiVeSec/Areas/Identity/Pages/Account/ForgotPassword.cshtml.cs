using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RegiVeSec.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                ViewData["Correo"] = user;
                var VehiculoRegiVeSec = (user);

                if (VehiculoRegiVeSec == null)
                {
                    ViewData["ErrorMessage"] = ($"El Correo : {user} no existe ");
                    ModelState.AddModelError(string.Empty, "El correo electronico no existe");
                    return Page();
                }
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    var To = Input.Email;
                    var From = "priscilaelisaguzman34@gmail.com";
                    //Ingrese su email
                    var Password = "valunchosehun19994"; //Ingrese Contraseña
                    var Puerto = 587;
                    var SMTP = "smtp.gmail.com";
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { code },
                        protocol: Request.Scheme);
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(From);// email del formulario

                    msz.To.Add(To); //correo que recive el mensaje
                    msz.Subject = "Restablecer Contraseña";// asunto del mensaje
                    msz.IsBodyHtml = true;// habilito formato html para que quede flama el correo
                    msz.Body = $@"
                               <strong>Restablecer Contraseña: </strong>{$"Por favor restablece tu contraseña <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo clic aquí</a>."}<br>
                               ";
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTP; //smtp varia depende del correo
                    smtp.Port = Puerto; // puerto varia depende el correo
                    smtp.Credentials = new System.Net.NetworkCredential(From, Password);// correo que va a enviar el mensaje y su contraseña gmail
                    smtp.EnableSsl = true;
                    smtp.Send(msz);
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
               




            }
            return Page();
        }
    }
}
