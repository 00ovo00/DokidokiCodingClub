using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [SerializeField] private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public TextAsset csvFile;
    public List<DialogueData> dialogues = new List<DialogueData>();

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        var lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            if (values.Length >= 13)
            {
                DialogueData data = new DialogueData
                {
                    DialogID = int.Parse(values[0]),
                    ID = int.Parse(values[1]),
                    Speaker = values[2],
                    Line = values[3],
                    IsOption = int.Parse(values[4]) == 1,
                    Question = values[5],
                    OptionA = values[6],
                    OptionB = values[7],
                    OptionC = values[8],
                    Result1 = int.Parse(values[9]),
                    Result2 = int.Parse(values[10]),
                    Result3 = int.Parse(values[11]),
                    NextDialogID = int.Parse(values[12])
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