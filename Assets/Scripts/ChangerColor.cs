using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerColor : MonoBehaviour
{
    public float distanceToPlayer;
    [SerializeField] Color backgroundColor;
    [SerializeField] Color principalLightColor;

    private float timer;
    private Color originalBackgroundColor;
    private Color originalPrincipalLightColor;
    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();

        originalBackgroundColor = Camera.main.backgroundColor;
        originalPrincipalLightColor = PrincipalLight.Instance.lightComponent.color;
    }

    public void ChangeBackground()
    {
        float playerDistance = Vector3.Distance(transform.position, Player.Instance.transform.position);
        if (playerDistance > distanceToPlayer) { return; }

        float t = (distanceToPlayer - playerDistance) / distanceToPlayer;

        Camera.main.backgroundColor = Color.Lerp(originalBackgroundColor, backgroundColor, t);
        PrincipalLight.Instance.lightComponent.color = Color.Lerp(originalPrincipalLightColor, principalLightColor, t);
    }

    private void Update()
    {
        ChangeBackground();
    }
}
