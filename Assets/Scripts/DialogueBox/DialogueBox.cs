using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [Header("Componentes")]
    public GameObject dialogObj;
    public Text speechText;
    public Text actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentence;
    private int index;
    private int click = 0;

    private string[] name_array;

    public void speech(string[] txt, string[] actorName)
    {
        actorNameText.text = "";
        speechText.text = "";
        name_array = actorName;
        sentence = txt;
        actorNameText.text = actorName[0];
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        
        StartCoroutine(TypeSentence());
        
    }
    IEnumerator TypeSentence()    // aparecer devagar
    {
        foreach (char letter in sentence[index].ToCharArray())
        {
            if (click == 0)
                speechText.text += letter;
            else
            {
                speechText.text = sentence[index];
                break;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        Debug.Log("next");
        addclick();
        if (string.Compare(speechText.text, sentence[index]) == 0)
        {
            if (index < sentence.Length - 1)
            {

                index++;
                actorNameText.text = name_array[index];
                speechText.text = "";
                click = 0;
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                index = 0;
                click = 0;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);

            }
        }

    }
    public void addclick()
    {
        click += 1;
        if (click > 1)
            click = 0;
    }

}
