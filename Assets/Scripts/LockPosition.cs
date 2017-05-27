using UnityEngine;

public class LockPosition : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Vector3.zero;
    }
}
