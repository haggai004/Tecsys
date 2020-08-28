using System;
using Tecsys.Retail.Ef;

namespace Tecsys.Retail.Repository
{
    public class Repository:IDisposable
    {
        public Ef.WingtiptoysEntities DbContext { get; }

        public Repository()
        {
            DbContext = new WingtiptoysEntities();
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
