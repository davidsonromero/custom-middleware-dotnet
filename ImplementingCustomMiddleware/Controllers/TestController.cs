﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingCustomMiddlewareApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Test()
        {
            return Ok("Success");
        }

        [HttpGet("exception")]
        public IActionResult TestException()
        {
            throw new ArgumentException("Test for argument exception");
        }
    }
}
