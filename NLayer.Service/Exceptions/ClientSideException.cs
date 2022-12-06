using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Exceptions
{
    public class ClientSideException:Exception
    {
        //Api'yı tüketen uygulamadan kaynaklanan hata.(client) 
        public ClientSideException(string message) : base(message) 
        {

        }
    }
}
