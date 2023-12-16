
using System.ComponentModel.DataAnnotations;

namespace Lms.SDK.Common
{
    /// <summary>
    /// Entity with Title property
    /// </summary>
    public interface IHasTitle : IEntity
    {
        /// <summary>
        /// Title
        /// </summary>
        [StringLength(250)]
        public string Title { get; set; }
    }
}

