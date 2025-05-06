using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
	internal class BadRequestException:Exception
	{
        public string? Details { get; set; }
        public BadRequestException(string message):base(message)
        {
            
        }

        public BadRequestException(string message,string details):base(message)
        {
            Details = details;
        }
    }
}
