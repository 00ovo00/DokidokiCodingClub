[System.Serializable]
public class DialogueData
{
    public int DialogID;        // 진행 중인 대화의 ID (for 에피소드 구분)
    public int ID;              // 대화 순서
    public string Speaker;      // 공략 대상
    public string Line;         // 대사
    public bool IsOption;       // 선택지인지 확인하는 플래그
    public string Question;
    public string OptionA;
    public string OptionB;
    public string OptionC;
    public int Result1;
    public int Result2;
    public int Result3;
    public int NextDialogID;    // 다음으로 진행할 대화의 ID
}