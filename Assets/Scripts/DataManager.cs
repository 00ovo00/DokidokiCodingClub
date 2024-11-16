using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    // csv 파일 데이터 불러오기. 

    [SerializeField] private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public TextAsset csvFile;
    public List<DialogueData> dialogues = new List<DialogueData>();

    public void LoadCSV(string csvFile)
    {
        TextAsset _data = Resources.Load<TextAsset>(csvFile);
        var lines = _data.text.Split('\n'); // 줄 단위로 잘라서 배열로 저장 

        for (int i = 1; i < lines.Length; i++) // 쉼표로 구분해서 배열로 저장 
        {
            var values = lines[i].Split(','); 
            if (values.Length >= 13) 
            {
                DialogueData data = new DialogueData
                {
                    DialogID = int.Parse(values[0]), // 대화 ID
                    ID = int.Parse(values[1]), // 다이어로그 아이디 내 문장 순서 
                    Speaker = values[2], // 발화자 
                    Line = values[3], // 내용 
                    IsOption = int.Parse(values[4]) == 1, // 분기점(선택지) 
                    Question = values[5], // 질문
                    OptionA = values[6], // 선택지1
                    OptionB = values[7], // 선택지2
                    OptionC = values[8], // 선택지3
                    Result1 = int.Parse(values[9]), // 호감도 결과
                    Result2 = int.Parse(values[10]), // 호감도 결과 
                    Result3 = int.Parse(values[11]), // 호감도 결과
                    NextDialogID = int.Parse(values[12]) // 다음 다이어로그 ID
                };
                dialogues.Add(data); 
            }
        }
    }
}

[System.Serializable]
public class DialogueData
{
    public int DialogID;
    public int ID;
    public string Speaker;
    public string Line;
    public bool IsOption;
    public string Question;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public int Result1;
    public int Result2;
    public int Result3;
    public int NextDialogID;
}