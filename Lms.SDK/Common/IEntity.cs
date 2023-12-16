using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lms.SDK.Common
{
    /// <summary>
    /// Database entity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}

