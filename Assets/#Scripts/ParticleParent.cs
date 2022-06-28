using UnityEngine;

public static class ParticleParent
{
	public static void ParentSet(this GameObject obj, Transform parent, float destroyTime = 0)
	{
		obj.transform.SetParent(parent);
		if (destroyTime != 0)
		{
			MonoBehaviour.Destroy(obj, destroyTime);
		}
	}
}