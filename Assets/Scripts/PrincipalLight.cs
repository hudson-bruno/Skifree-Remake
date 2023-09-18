using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincipalLight : MonoBehaviour
{
    public static PrincipalLight Instance { get; private set; }
    public Light lightComponent;
    private void Awake()
    {
        lightComponent = GetComponent<Light>();

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
