using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 4, -6);
    void LateUpdate()
    {
        if (target)
            transform.position = target.position + offset;
    }
}
