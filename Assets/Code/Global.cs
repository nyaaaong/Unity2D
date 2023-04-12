using UnityEngine;

public class Global : MonoBehaviour
{
	public Vector2 ConvertDir(float angle)
	{
		Vector2 Result;

		angle *= Mathf.Deg2Rad;

		Result = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		Result.Normalize();

		return Result;
	}
}
