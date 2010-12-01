﻿using Portoa.Util;

namespace Portoa.Persistence {
	public abstract class Entity<T, TId> : IIdentifiable<TId> where T : Entity<T, TId> {
		private int? originalHashCode;

		public virtual TId Id { get; set; }

		public override bool Equals(object obj) {
			var comparisonObj = obj as T;

			if (comparisonObj == null) {
				return false;
			}

			var comparisonIsTransient = Equals(comparisonObj.Id, default(TId));
			var thisIsTransient = Equals(Id, default(TId));

			if (comparisonIsTransient && thisIsTransient) {
				return ReferenceEquals(comparisonObj, this);
			}

			return comparisonObj.Id.Equals(Id);
		}

		public override int GetHashCode() {
			if (originalHashCode.HasValue) {
				return originalHashCode.Value;
			}

			var thisIsTransient = Equals(Id, default(TId));

			if (thisIsTransient) {
				originalHashCode = base.GetHashCode();
				return originalHashCode.Value;
			}

			return Id.GetHashCode();
		}

		public override string ToString() {
			return string.Format("{0}(Id={1})", GetType().GetFriendlyName(false), Id);
		}

		public static bool operator ==(Entity<T, TId> left, Entity<T, TId> right) {
			return Equals(left, right);
		}

		public static bool operator !=(Entity<T, TId> left, Entity<T, TId> right) {
			return !(left == right);
		}
	}
}