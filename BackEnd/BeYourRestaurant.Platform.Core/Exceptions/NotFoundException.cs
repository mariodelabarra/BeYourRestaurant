using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYourRestaurant.Platform.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        private const string Message = "The entity {0} with Id '{1}' could not be found";

        public NotFoundException(string entity, string id) : base(string.Format(Message, entity, id))
        {
        }
    }
}
