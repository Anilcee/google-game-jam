using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position + offset;
            newPosition.z = -10;
            transform.position = newPosition;
        }
    }
}

