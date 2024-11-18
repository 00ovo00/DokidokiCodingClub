using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    public Dialogue[] dialogues;
    public DialogueData Parse(string FileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(FileName);
        if (jsonFile == null) 
        {
            Debug.LogError($"파일 {FileName}을 찾을 수 없습니다.");
            return null;
        }
        DialogueData dialoguedata = JsonUtility.FromJson<DialogueData>(jsonFile.text); // 역직렬화
        return dialoguedata;
    }

    private void Start()
    {
        DialogueData dialogueData = Parse("TestData");

        if (dialogueData != null && dialogueData.dialogues.Length > 0)
        {
            dialogues = dialogueData.dialogues;
            Debug.Log(dialogues[0]);
    }
        else
        {
            Debug.LogError("No dialogues found.");
        }
    }
}







