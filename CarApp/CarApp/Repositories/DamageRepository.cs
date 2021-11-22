using System;
using System.Collections.Generic;
using System.Text;
using CarApp.Interfaces;
using CarApp.Models;
using SQLite;

namespace CarApp.Repositories
{
    public class DamageRepository: Repository<Damage>, IDamageRepository
    {
        public DamageRepository(SQLiteAsyncConnection db) : base(db)
        {
        }
    }
}
