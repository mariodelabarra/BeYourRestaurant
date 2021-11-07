using System;

namespace BeYourRestaurant.Platform.Core.Domain
{
    /// <summary>
    /// Represent the common properties that the entities have
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
