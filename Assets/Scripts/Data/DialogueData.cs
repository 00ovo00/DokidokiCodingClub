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
}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogues;
}


