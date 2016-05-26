using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Swas.Clients.Common
{
    public class CustomerTypeDescriotion
    {
        public Dictionary<int, string> Description
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    {0,"მუნიციპალიტეტი" },
                    {1,"იურიდიული პირი" },
                    {2,"ფიზიკური პირი" },
                };
            }
        }
    }

}
