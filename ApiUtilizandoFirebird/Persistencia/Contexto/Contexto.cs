using System;
using System.Linq;
using ApiUtilizandoFirebird.Persistencia.Base;
using ApiUtilizandoFirebird.Persistencia.Database.Entity;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiUtilizandoFirebird.Persistencia.Contexto
{
    public sealed class Contexto: DbContext
    {
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfiguration(new UF.UFConfiguration());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseLazyLoadingProxies();
				var conStr = "";
				optionsBuilder.UseFirebird(conStr);
			}
		}

		#region IDbContext Members

		public void Delete<TEntity>(TEntity entidade) where TEntity : ModelBase
		{
			base.Remove<TEntity>(entidade);
		}

		public TEntity FindById<TEntity>(int id) where TEntity : ModelBase
		{
			return Set<TEntity>().Where(p => p.ID == id).OrderBy(p => p.ID).ToList().FirstOrDefault();
		}

		#endregion

		#region Override

		public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
		{
			return base.Add(entity);
		}

		public override EntityEntry<TEntity> Attach<TEntity>(TEntity entity)
		{
			return base.Attach(entity);
		}

		public override DbSet<TEntity> Set<TEntity>()
		{
			return base.Set<TEntity>();
		}

		public new bool SaveChanges()
		{
			return (base.SaveChanges() > 0);
		}

		#endregion

		private string ConStr
        {
            get
            {
				return $"User=SYSDBS;Password=masterkey;Database=BANCO.FB;DataSource=localhost;Port=3050;" +
					$"Dialect=3;Charset=WIN1252;Role=;Connection lifetime=0;" +
					$"Pooling=0;MinPoolSize=0;MaxPoolSize=False;PacketSize=8196;" +
					$"ServerType=1;";
			}
        }
    }
}
