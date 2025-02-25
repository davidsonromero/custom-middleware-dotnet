using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementingCustomMiddlewareApplication.DTOs.Login
{
    public sealed record LoginResponseDto
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public int MinutesToExpire { get; set; }
    }
}
