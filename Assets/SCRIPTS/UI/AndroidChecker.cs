using UnityEngine;

public class AndroidChecker : MonoBehaviour
{
    private void Awake()
    {
#if !UNITY_ANDROID
        DestroyImmediate(gameObject);
#endif
    }
}