using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Portoa.Validation.DataAnnotations {
	internal sealed class ReflectionCache {
		private static readonly ReflectionCache Singleton = new ReflectionCache();
		//private readonly ICollection<>

		public static ReflectionCache Instance { get { return Singleton; } }

		public FieldInfo GetField(ValidationContext context) {

			var name = context.MemberName;
			return null;

		}

		//private MemberInfo GetItem() {

		//}

	}
}