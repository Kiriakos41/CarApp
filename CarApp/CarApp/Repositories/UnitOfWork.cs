using CarApp.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SQLiteAsyncConnection context)
        {

        }


        public void Dispose()
        {
            return;
        }
    }
}
