[System.Serializable]   
public class Dialogue
{
    public int ID;
    public string name;
    public string[] lines;
    public bool isOption;
    public string Question;
    public string[] Options;
    public string[] Target;
    public int[] Results;
    public int[] Param;
    public string[] BGImage;
    public string[] CharImage;


}

[System.Serializable]
public class DialogueData
{
    public Dialogue[] dialogues;
}


