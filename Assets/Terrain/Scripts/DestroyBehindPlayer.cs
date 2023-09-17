using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehindPlayer : MonoBehaviour
{
    public float distanceBehindToDestroy = 200f;
    void Update()
    {
        if (Vector3.Distance(Player.Instance.transform.position, transform.position ) < distanceBehindToDestroy)
        {
            Destroy(this);
        } 
    }
}
