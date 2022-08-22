using UnityEngine;

public class CopyMove : MonoBehaviour
{
	public Transform Target;

	// Update is called once per frame
	void LateUpdate() => transform.position = Target.position;
}