namespace Swas.Business.Logic.Common
{
    using Data.Access.Context;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BusinessLogicBase
    {
        private DataContext _context;

        public DataContext Context
        {
            get
            {
                if (_context == null)
                    _context = new DataContext();

                return _context;
            }
        }

        public void Connect()
        {
            Context.OpenConection();
        }

        public void Dispose()
        {
            Context.CloseConnection();
            Context.Dispose();
        }

        public BusinessLogicBase()
        {

        }

        public BusinessLogicBase(DataContext context)
        {
            _context = context;
        }
    }

}
