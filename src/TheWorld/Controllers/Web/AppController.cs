using Microsoft . AspNetCore . Mvc;
using TheWorld . ViewModels;
using TheWorld . Services;
using Microsoft . Extensions . Configuration;
using TheWorld . Models;
using Microsoft . Extensions . Logging;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld . Controllers . Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepo _repo;
        private ILogger<AppController> _logger;

        public AppController ( IMailService mailService,IConfigurationRoot config,Models.IWorldRepo repo,ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repo = repo;
            _logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index ( )
        {
            try
            {
                var data = _repo.GetAllTrips();
                return View ( data );
            }
            catch ( System . Exception ex)
            {

                _logger . LogError ( $"An error occured while fetching trips:{ex . Message}" );
                return Redirect ( "/error" );
            }
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
