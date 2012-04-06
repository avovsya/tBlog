using System;
using System.ComponentModel.DataAnnotations;

namespace tBlog.Domain
{
    public class Entity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        private int _transientHashCode;

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }

        public virtual bool Equals(Entity obj)
        {
            if (obj == null) return false;

            if (IsTransient)
            {
                return ReferenceEquals(this, obj);
            }

            return obj.Id == Id && obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            if (IsTransient)
            {
                if (_transientHashCode == 0)
                {
                    _transientHashCode = base.GetHashCode();
                }
                return _transientHashCode;
            }
            return Id;
        }

        private bool IsTransient
        {
            get { return Id == 0; }
        }
    }

    public class EntityWithTimestamps : Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
