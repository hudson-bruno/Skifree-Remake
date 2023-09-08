using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class StatsPlayer : MonoBehaviour
{
    public GameObject game_over_ui;
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Monster")
        {
            GameOver();
            gameObject.SetActive(false);
        }
    }
    void GameOver()
    {
        Debug.Log(game_over_ui.name);
        game_over_ui.SetActive(true);
    }
}
