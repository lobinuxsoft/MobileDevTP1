using UnityEngine;

public class BlobShadowController : MonoBehaviour
{
	[SerializeField] float heigh = 8.246965f;

    void Update()
	{
		transform.position = transform.parent.position + Vector3.up * heigh;
		transform.rotation = Quaternion.LookRotation(-Vector3.up, transform.parent.forward);
	}
}