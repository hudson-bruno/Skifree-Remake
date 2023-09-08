using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button ButtonTryAgain;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        ButtonTryAgain.onClick.AddListener(TryAgainButton);
    }

    void TryAgainButton()
    {
        SceneManager.LoadScene("Game");
    }
}
