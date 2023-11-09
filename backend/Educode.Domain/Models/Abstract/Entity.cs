using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educode.Domain.Models.Abstract
{
    public abstract class Entity<TId>:IComparable, IComparable<Entity<TId>> where TId : IComparable<TId>
    {
        public virtual TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            Entity<TId> entity = obj as Entity<TId>;
            if ((object)entity == null)
            {
                return false;
            }

            if ((object)this == entity)
            {
                return true;
            }

            if (ValueObject.GetUnproxiedType(this) != ValueObject.GetUnproxiedType(entity))
            {
                return false;
            }

            if (IsTransient() || entity.IsTransient())
            {
                return false;
            }

            return Id.Equals(entity.Id);
        }

        private bool IsTransient()
        {
            if (Id != null)
            {
                return Id.Equals(default(TId));
            }

            return true;
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if ((object)a == null && (object)b == null)
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (ValueObject.GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }


        public int CompareTo(object? obj)
        {
            return CompareTo(obj as Entity<TId>);
        }

        public int CompareTo(Entity<TId>? other)
        {
            if (other == null) return 1;
            if (other == this) return 0;

            return Id.CompareTo(other.Id);
        }
    }
}
