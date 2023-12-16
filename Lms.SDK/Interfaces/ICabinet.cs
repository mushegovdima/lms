using Lms.SDK.Common;

namespace Lms.SDK.Interfaces
{
    /// <summary>
    /// Cabinet 
    /// </summary>
    public interface ICabinet : IEntity, IHasTitle
    {
        /// <summary>
        /// Author Id
        /// </summary>
        public long AuthorId { get; set; }

        /// <summary>
        /// Created at timestamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }
    }
}

