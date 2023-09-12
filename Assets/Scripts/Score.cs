using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Rigidbody playerRigidbody;

    public TextMeshProUGUI Points;
    public TextMeshProUGUI Textkm_h;

    private void Update()
    {
        float distance = Mathf.Floor(playerRigidbody.transform.position.z);
        Points.text = "Distance: " + (distance > 0 ? distance : 0);

        float kmH = Mathf.Floor(Mathf.Abs(playerRigidbody.velocity.z) * 3.6f);
        Textkm_h.text = "Km/h: " + (distance > 0 ? kmH : 0);
    }
}
