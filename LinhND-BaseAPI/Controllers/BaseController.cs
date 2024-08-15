using Microsoft.AspNetCore.Mvc;
using Service.Services.Base;

namespace LinhND_BaseAPI.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected readonly IServiceManager _serviceManager;


        public BaseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }   

    }
}
