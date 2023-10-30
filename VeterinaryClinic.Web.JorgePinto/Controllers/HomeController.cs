using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using VeterinaryClinic.Web.JorgePinto.Data.Entities;
using VeterinaryClinic.Web.JorgePinto.Helpers;
using VeterinaryClinic.Web.JorgePinto.Models;

namespace VeterinaryClinic.Web.JorgePinto.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUserHelper _userHelper;
		private readonly IMailHelper _mailHelper;
		private readonly IConfiguration _configuration;

		public HomeController(ILogger<HomeController> logger, IUserHelper userHelper, IMailHelper mailHelper, IConfiguration configuration)
		{
			_logger = logger;
			_userHelper = userHelper;
			_mailHelper = mailHelper;
			_configuration = configuration;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Contact()
		{
			return View();
		}
        /// <summary>
        ///TODO FAZER VALIDAÇÃO SE ESTIVER PREENCHIDO OU UMA CHECK BOX PARA ENVIAR SOMENTE A UMA PESSOA
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
		[HttpPost]
		public IActionResult SendContact(ContactViewModel model)
		{
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid information!";
                return View("Contact", model);
            }

            try
            {
                string senderEmail = "emailwebprovidercodingwooo@gmail.com";
                string senderPassword = "xgmuryodfglgnfky";

                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    client.EnableSsl = true;

                    var emailMessage = new MailMessage
                    {
                        Subject = "Admin ClinicVet",
                        From = new MailAddress(senderEmail),
                        IsBodyHtml = true,
                        Body = model.Message
                    };

                    // Lista de destinatários (adicionando o e-mail do modelo)
                    var recipientEmails = new List<string>
                        {
                            "felypperau@gmail.com",

                            "marianaleite@yopmail.com",
                            "filipebaptista@yopmail.com",
                            "mariasilva@yopmail.com",
                            "brunoferreira@yopmail.com",
                            "simaoandre@yopmail.com",
                            "luispatricio@yopmail.com",
                            "dariodias@yopmail.com",
                            "laerciosantos@yopmail.com",
                            "tatianeavellar@yopmail.com",
                            "claudiabird@yopmail.com",
                            "nunosantos@yopmail.com",
                            "catarinapalma@yopmail.com",
                            "diogoalves@yopmail.com",
                            "marianaoliveira@yopmail.com",
                            "reinaldosouza@yopmail.com",
                            "rubencorreia@yopmail.com",
                            "duartemarques@yopmail.com",
                            "liciniorosario@yopmail.com",
                            "joelrangel@yopmail.com",
                            "pedrosilva@yopmail.com",
                            "luisleopoldo@yopmail.com",
                            "anaribeiro@yopmail.com",
                        };

                    // Adicione os e-mails da ViewModel aos destinatários
                    //if (!string.IsNullOrWhiteSpace(model.Email))
                    //{
                    //    recipientEmails.Add(model.Email);
                    //}

                    if (!string.IsNullOrWhiteSpace(model.Email))
                    {
                        // Divida os endereços de e-mail usando vírgulas como separadores
                        var emails = model.Email.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var email in emails)
                        {
                            // Adicione cada endereço de e-mail à lista de destinatários
                            recipientEmails.Add(email.Trim()); // Remova espaços em branco
                        }
                    }

                    // Adicione os destinatários à mensagem
                    foreach (string recipient in recipientEmails)
                    {
                        emailMessage.Bcc.Add(recipient);
                    }

                    // Envie o e-mail
                    client.Send(emailMessage);

                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Error while sending the email: " + ex.Message;
                return View("Contact", model);
            }



            //if (!ModelState.IsValid)
            //{
            //	ViewData["Message"] = "Invalid information!";
            //	return View("Contact", model);
            //}

            ////https://www.youtube.com/watch?v=FN2PjZHlmKE  -  16:04


            //{
            //	var emailMessage = new MailMessage();
            //	emailMessage.Subject = "Admin ClinicVet";
            //	emailMessage.From = new MailAddress("emailwebprovidercodingwooo@gmail.com");
            //	//emailMessage.To.Add("felypperau@gmail.com");
            //	string[] recipientEmails = new string[]
            //	{
            //		"felypperau@gmail.com",

            //		"marianaleite@yopmail.com",
            //		"filipebaptista@yopmail.com",
            //		"mariasilva@yopmail.com",
            //		"brunoferreira@yopmail.com",
            //		"simaoandre@yopmail.com",
            //		"luispatricio@yopmail.com",
            //		"dariodias@yopmail.com",
            //		"laerciosantos@yopmail.com",
            //		"tatianeavellar@yopmail.com",
            //		"claudiabird@yopmail.com",
            //		"nunosantos@yopmail.com",
            //		"catarinapalma@yopmail.com",
            //		"diogoalves@yopmail.com",
            //		"marianaoliveira@yopmail.com",
            //		"reinaldosouza@yopmail.com",
            //		"rubencorreia@yopmail.com",
            //		"duartemarques@yopmail.com",
            //		"liciniorosario@yopmail.com",
            //		"joelrangel@yopmail.com",
            //		"pedrosilva@yopmail.com",
            //		"luisleopoldo@yopmail.com",
            //		"anaribeiro@yopmail.com",

            //	};

            //	foreach (string recipient in recipientEmails)
            //	{
            //		emailMessage.Bcc.Add(recipient);
            //	}
            //	emailMessage.IsBodyHtml = true;

            //	emailMessage.Body = model.Message;

            //	var client = new SmtpClient("smtp.gmail.com", 587);

            //	client.Credentials = new NetworkCredential("emailwebprovidercodingwooo@gmail.com", "xgmuryodfglgnfky");
            //	client.EnableSsl = true;

            //	try
            //	{
            //		client.Send(emailMessage);
            //	}

            //	catch (Exception ex)
            //	{
            //		ViewData["Message"] = "Error while resetting the password" + ex.Message;
            //	}


            //	return View();
            //}

            //if (this.ModelState.IsValid)
            //{
            //	var user = await _userHelper.GetUserByEmailAsync(model.Email);

            //	if (user == null)
            //	{
            //		ModelState.AddModelError(string.Empty, "The email doesn't correspond to a registered user.");
            //		return View(model);
            //	}

            //	var link = this.Url.Action(
            //		"Contact",
            //		"Home",
            //		new {}, protocol: HttpContext.Request.Scheme);

            //	Response response = _mailHelper.SendEmail(model.Email, "Appointment confirmation", $"<h2>Já está confirmada</h2>" /*+*/
            //	//$"To reset the password click in this link:</br></br>" +
            //	/*$"<a href = \"{link}\">Reset Password</a>"*/);

            //	if (response.IsSuccess)
            //	{
            //		this.ViewBag.Message = "The instructions to recover your password has been sent to email.";
            //	}

            //	return this.View();
            //}

            //return this.View(model);

        }
	}
}
