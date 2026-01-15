using UnityEngine;

namespace Utilities.Pool
{
	public interface IPoolable
	{
		ObjectType Type { get; set; }
		bool IsInThePool { get; set; }
		GameObject GameObject => ((MonoBehaviour)this).gameObject;
		void OnSpawn();
		void OnReset();
	}
	
	public static class IPoolableExtensions
	{
		public static bool ResetObject(this IPoolable poolable, bool removeFromActiveObjects=true)
		{
			if (PoolFactory.Instance)
			{
				return PoolFactory.Instance.ResetObject(poolable,removeFromActiveObjects);	
			}
			return false;
		}
	}
}