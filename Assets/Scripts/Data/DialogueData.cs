[System.Serializable]   
public class Dialogue
{
    public int ID;
    public string name;
    public string[] lines;
    public bool isOption;
    public string Question;
    public string[] Options;
    public int[] Results;
    public override string ToString() 
    {
        // 예시로 name과 ID만 출력하도록 정의
        return $"ID: {ID}, Name: {name}, IsOption: {isOption}, Question: {Question}";
    }
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogues;
}


