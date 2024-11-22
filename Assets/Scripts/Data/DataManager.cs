using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
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
}







