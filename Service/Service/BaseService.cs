using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class BaseService
    {
        public readonly DataContext DbContext;

        public BaseService(DataContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
