using System.ComponentModel.DataAnnotations;
using Lms.SDK.Common;

namespace Lms.SDK.Interfaces
{
    /// <summary>
    /// User
    /// </summary>
    public interface IUser : IEntity
    {
        /// <summary>
        /// Email
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [StringLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}

