using CarApp.Interfaces;
using SQLite;

namespace CarApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(SQLiteAsyncConnection context)
        {
            AboutCars = new AboutCarRepository(context);
            ServiceTables = new ServiceTableRepository(context);
            Damages = new DamageRepository(context);
            RubberTables = new RuberRepository(context);
            ExtraTables = new ExtraRepository(context);
            KteoTables = new KteoRepository(context);
            DistanceTable = new DistanceRepository(context);
            ProfileTb = new ProfileRepository(context);
            ProtectionTables = new ProtectionRepository(context);
        }

        public IProtectionRepository ProtectionTables { get; }
        public IAboutCarRepository AboutCars { get; }
        public IDamageRepository Damages { get; }
        public IServiceTableRepository ServiceTables { get; }
        public IRubberRepository RubberTables { get; }
        public IExtraRepository ExtraTables { get; }
        public IKteoRepository KteoTables { get; }
        public IDistanceRepository DistanceTable { get; }
        public IProfileRepository ProfileTb { get; }

        public void Dispose()
        {
            return;
        }
    }
}
