using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
