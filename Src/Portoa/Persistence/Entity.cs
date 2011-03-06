using Portoa.Util;

namespace Portoa.Persistence {
	/// <summary>
	/// Represents a domain object that can be persisted by a <c cref="IRepository{T, TId}">repository</c>
	/// </summary>
	/// <typeparam name="TId">The entity's identifier type</typeparam>
	public abstract class Entity<TId> : IIdentifiable<TId> {
		public virtual TId Id { get; set; }

		public override bool Equals(object obj) {
			if (obj == null || obj.GetType() != GetType()) {
				return false;
			}

			var comparisonObj = (IIdentifiable<TId>)obj;

			if (comparisonObj.IsTransient() && this.IsTransient()) {
				return ReferenceEquals(obj, this);
			}

			return comparisonObj.Id.Equals(Id);
		}

		public override int GetHashCode() {
			if (this.IsTransient()) {
				return base.GetHashCode();
			}

			return Id.GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0}(Id={1})", GetType().GetFriendlyName(false), Id);
		}

		public static bool operator ==(Entity<TId> left, Entity<TId> right) {
			return Equals(left, right);
		}

		public static bool operator !=(Entity<TId> left, Entity<TId> right) {
			return !(left == right);
		}
	}
}