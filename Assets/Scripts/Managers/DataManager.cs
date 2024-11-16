using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [SerializeField] private int _score = 0;    // 호감도
    // TODO: 공략 대상 데이터 만들어서 분리하기

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private TextAsset _csvFile;   // 엑셀 시트에서 가져온 원본 데이터
    public List<DialogueData> Dialogs = new List<DialogueData>();

    protected override void Awake()
    {
        base.Awake();
        LoadCSV();
    }

    private void LoadCSV()
    {
        var lines = _csvFile.text.Split('\n');  // 엑셀 시트 행 분리
        for (int i = 1; i < lines.Length; i++)  // 0행의 열 정보 제외
        {
            var values = lines[i].Split(',');  // 엑셀 시트 열 분리
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
                Dialogs.Add(data);
            }
        }
    }
}