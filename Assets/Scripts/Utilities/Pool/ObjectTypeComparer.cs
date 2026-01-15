using System.Collections.Generic;

namespace Utilities.Pool
{
	public class ObjectTypeComparer : IEqualityComparer<ObjectType>
	{
		public bool Equals(ObjectType x, ObjectType y)
		{
			return x == y;
		}
		
		public int GetHashCode(ObjectType obj)
		{
			return (int)obj;
		}
	}
}