using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HostessBehaviour : MonoBehaviour, IInteractable
{
    public TextAsset jsonDialogues;
    string[] speech, names;
    GameObject dialogueControle;
    public Transform toolTipPosition;

    private void Awake()
    {
        (speech, names) = JsonLoad.LoadJson(jsonDialogues);
        dialogueControle = GameObject.Find("DialogueControle");
    }
    public void Interact(GameObject interactor)
    {
      
        dialogueControle.GetComponent<DialogueBox>().speech(speech, names);

        
        
    }
    public Vector3 GetToolTipPosition()
    {
        return toolTipPosition.position;
    }
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (dialogueControle.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
            {
                dialogueControle.GetComponent<DialogueBox>().NextSentence();
            }
        }
    }

}
