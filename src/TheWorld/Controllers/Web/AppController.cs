using Microsoft . AspNetCore . Mvc;
using TheWorld . ViewModels;
using TheWorld . Services;
using Microsoft . Extensions . Configuration;
using TheWorld . Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld . Controllers . Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepo _repo;

        public AppController ( IMailService mailService,IConfigurationRoot config,Models.IWorldRepo repo)
        {
            _mailService = mailService;
            _config = config;
            _repo = repo;
        }
        // GET: /<controller>/
        public IActionResult Index ( )
        {
            var data = _repo.GetAllTrips();
            return View ( data );
        }

        public IActionResult Contact ( )
        {
            return View ( );
        }

        [HttpPost]
        public IActionResult Contact ( ContactViewModel model )
        {
            if ( ModelState . IsValid )
            {
                _mailService . SendMail ( _config [ "MailSettings:ToAddress" ] , model . Email , "From The world" , model . Message );
                ModelState . Clear ( );
                ViewBag . UserMessage = "Message sent.";
            }
            
            return View ( );
        }

        public IActionResult About ( )
        {
            return View ( );
        }
    }
}
