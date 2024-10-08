﻿using Microsoft.AspNetCore.Mvc;
using Service.Helpers;
using Service.Services.Base;

namespace LinhND_BaseAPI.Controllers
{
    [Route("/api/v1")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        protected readonly IServiceManager _serviceManager;

        protected readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }   

    }
}
