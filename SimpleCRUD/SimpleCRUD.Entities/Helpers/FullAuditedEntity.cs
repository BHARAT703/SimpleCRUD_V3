using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Entities.Helpers
{
    public interface IFullAuditedEntity
    {
        object Id { get; set; }
        DateTime CreationDateTime { get; set; }
        long? CreationUserId { get; set; }
        DateTime? LastModificationDateTime { get; set; }
        long? LastModificationUserId { get; set; }
        bool DeletionFlag { get; set; }
        long? DeletionUserId { get; set; }
        DateTime? DeletionDateTime { get; set; }
    }

    public abstract class FullAuditedEntity<T> : IFullAuditedEntity
    {
        public FullAuditedEntity() { }

        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public virtual T Id { get; set; }

        object IFullAuditedEntity.Id
        {
            get { return Id; }
            set
            {
                Id = (T)value;
            }
        }

        /// <summary>
        /// Creation time of this entity.
        /// </summary>        
        public virtual DateTime CreationDateTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// </summary>
        public virtual long? CreationUserId { get; set; }

        /// <summary>
        /// Last modification date of this entity.
        /// </summary>        
        public virtual DateTime? LastModificationDateTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual long? LastModificationUserId { get; set; }

        /// <summary>
        /// Is this entity Deleted?
        /// </summary>
        public virtual bool DeletionFlag { get; set; }

        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        public virtual long? DeletionUserId { get; set; }

        /// <summary>
        ///  Deletion time of this entity.
        /// </summary>
        public virtual DateTime? DeletionDateTime { get; set; }
    }
}
