using ApiUtilizandoFirebird.Persistencia.Base;
using ApiUtilizandoFirebird.Persistencia.Database.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ApiUtilizandoFirebird.Persistencia.Database.Entity
{
    public class UF: ModelBase
    {
		#region	Campos privados

		private string fSigla;
		private string fNome;
		#endregion

		#region	Construtor

		public UF()
		{
			fSigla = string.Empty;
			fNome = string.Empty;
		}

		#endregion

		#region Propriedades

		public string Sigla
		{
			get { return fSigla; }
			set
			{
				if (value.Trim().Length > 2)
				{
					throw new ArgumentOutOfRangeException(String.Format("Valor nulo nao e permitido para Sigla. MaxLength DB: 2. MaxLength recebido: {0}. Tabela: {1}. ID: {2}.", value.Trim().Length, "UF.cs", this.ID), value, value);
				}
				fSigla = value;
			}
		}

		public string Nome
		{
			get { return fNome; }
			set
			{
				if (value.Trim().Length > 80)
				{
					throw new ArgumentOutOfRangeException(String.Format("Valor nulo nao e permitido para Nome. MaxLength DB: 80. MaxLength recebido: {0}. Tabela: {1}. ID: {2}.", value.Trim().Length, "UF.cs", this.ID), value, value);
				}
				fNome = value;
			}
		}

		#endregion

		#region Configuration

		public class UFConfiguration : IEntityTypeConfiguration<UF>
		{
			public void Configure(EntityTypeBuilder<UF> builder)
			{
				builder.HasKey(p => p.ID);
				builder.Property(p => p.ID).HasColumnName("ID").IsRequired().HasValueGenerator<IDGenerator>();
				builder.Property(p => p.Sigla).HasColumnName("SIGLA").HasMaxLength(2).IsUnicode(false);
				builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(80).IsUnicode(false);
				builder.ToTable("UF");
			}
		}

		#endregion
	}
}
