using UnityEngine;

public class ToggleObjectTrigger : MonoBehaviour
{
	Renderer rend;

	void Awake() 
	{
		rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    void OnTriggerEnter() => rend.enabled = true;
	
	void OnTriggerExit() => rend.enabled = false;
}