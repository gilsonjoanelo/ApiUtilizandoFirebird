using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUtilizandoFirebird.Persistencia.Base
{
    public class ModelBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
    }
}
