using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace ApiUtilizandoFirebird.Persistencia.Database.Generators
{
    internal class IDGenerator : ValueGenerator
    {
        public override bool GeneratesTemporaryValues => false;

        protected override object NextValue(EntityEntry entry)
        {
            if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                var vObjeto = new Persistencia.Database.ExecuteSql();
                var vSql = "SELECT GEN_ID(GEN_INTERNO_CADASTRO, 1) FROM RDB$DATABASE";
                return vObjeto.GenID(entry.Context.Database.GetDbConnection(), vSql);
            }
            var vCampo = entry.Entity.GetType().GetProperty("ID");
            var vValor = vCampo.GetValue(entry.Entity);
            return Convert.ChangeType(vValor, vCampo.PropertyType);
        }
    }
}
