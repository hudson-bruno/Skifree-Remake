using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public Transform InicialPoint;
    public Transform PlayerPoint;

    public TextMeshProUGUI Points;
    public TextMeshProUGUI Textkm_h;

    private float Current_z;
    public float km_h;

    private float countsec = 0;
    void Start()
    {
        Current_z = InicialPoint.transform.position.z;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Points.text =("Distance: ")+ Mathf.Round(Mathf.Abs((InicialPoint.transform.position.z - PlayerPoint.transform.position.z)/10)).ToString();

        KM_h();
    }

    void KM_h()
 
    {
        countsec += Time.deltaTime;
        if (countsec > 1)
        {
            km_h = Mathf.Round(((Current_z - PlayerPoint.transform.position.z) * -360) / 100);

            Current_z = PlayerPoint.transform.position.z;

            Textkm_h.text = "Km/h: " + km_h.ToString();
            countsec = 0;
        }


    }
}
