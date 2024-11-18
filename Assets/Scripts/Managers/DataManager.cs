using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    // csv ���� ������ �ҷ�����. 

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
        var lines = _data.text.Split('\n'); // �� ������ �߶� �迭�� ���� 

        for (int i = 1; i < lines.Length; i++) // ��ǥ�� �����ؼ� �迭�� ���� 
        {
            var values = lines[i].Split(','); 
            if (values.Length >= 13) 
            {
                DialogueData data = new DialogueData
                {
                    DialogID = int.Parse(values[0]), // ��ȭ ID
                    ID = int.Parse(values[1]), // ���̾�α� ���̵� �� ���� ���� 
                    Speaker = values[2], // ��ȭ�� 
                    Line = values[3], // ���� 
                    IsOption = int.Parse(values[4]) == 1, // �б���(������) 
                    Question = values[5], // ����
                    OptionA = values[6], // ������1
                    OptionB = values[7], // ������2
                    OptionC = values[8], // ������3
                    Result1 = int.Parse(values[9]), // ȣ���� ���
                    Result2 = int.Parse(values[10]), // ȣ���� ��� 
                    Result3 = int.Parse(values[11]), // ȣ���� ���
                    NextDialogID = int.Parse(values[12]) // ���� ���̾�α� ID
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