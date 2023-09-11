using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class JsonLoad : MonoBehaviour
{
    public TextAsset json_file;

    [System.Serializable]
    public class Npc_Dialogue
    {
        public int progress;
        public List<Dialogue> dialogs;

    }
    [System.Serializable]
    public class Dialogue
    {
        public string speech;
        public string name_dialogue_box;
    }

    public static (string[], string[]) LoadJson (TextAsset json_file) 
    {       
        Npc_Dialogue json = JsonUtility.FromJson<Npc_Dialogue>(json_file.text);
        string[] speecht = new string[json.dialogs.Count];
        string[] namet = new string[json.dialogs.Count];
        for (int i = 0; i < json.dialogs.Count; i++)
        {
          speecht[i] = json.dialogs[i].speech;
            namet[i] = json.dialogs[i].name_dialogue_box;
        }

        return (speecht, namet);
    }
}
