using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangerColor : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float lerp_time;
    [SerializeField] Color my_color;
    [SerializeField] Color my_color_light;
    public Light light;
    void Start()
    {
        Camera camera = gameObject.GetComponent<Camera>();
    }

    public void ChangeBackground()
    {
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, my_color, lerp_time);
        light.color = Color.Lerp(light.color, my_color_light, lerp_time);

    }
}
