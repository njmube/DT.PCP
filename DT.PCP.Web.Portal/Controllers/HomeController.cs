using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using DT.PCP.BussinesServices;
using DT.PCP.DataAccess;
using DT.PCP.Domain;
using DT.PCP.Logging;
using DT.PCP.ServicesProxies.BddViolationService;
using DT.PCP.Web.ViewModels.Home;

namespace DT.PCP.Web.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly IRepository _repository;
        private readonly ITrafficViolationService _trafficViolationService;
        private readonly IViolationService _violationService;

        public HomeController(ILogger logger, IEmailService emailService, IRepository repository,ITrafficViolationService trafficViolationService, IViolationService violationService)
        {
            _logger = logger;
            _emailService = emailService;
            _repository = repository;
            _trafficViolationService = trafficViolationService;
            _violationService = violationService;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel
                {
                    PayedViolationCount = _repository.Query<OrderDetail>().Count(d => d.Order.IsPayed) + int.Parse(ConfigurationManager.AppSettings["StartCount"])
                };

            return View(model);
        }

        
    }
}
