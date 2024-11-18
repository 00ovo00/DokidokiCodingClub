using System.IO;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData dialogueData;
    public string fileName;

    [ContextMenu("To Json Data")] 
    private void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(dialogueData, true);
        string path = Path.Combine(Application.dataPath, $"{fileName}.json");
        File.WriteAllText(path, jsonData);
    }
}


