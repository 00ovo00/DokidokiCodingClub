using System;
using UnityEngine;

public partial class DialogueUI : UIBase
{
    // 대화창 호출 
    private void Start()
    {
        // 여기서 데이터 넘겨받기.
        Debug.Log("다이얼로그 UI를 찾았습니다.");
    }

    public void PopUpMenu(int Index)
    {
        switch (Index)
        {
            case 0:
                UIManager.Instance.Show<Popup001>();
                break;
            case 1:
                UIManager.Instance.Show<Popup002>();
                break;
            case 2:
                UIManager.Instance.Show<Popup003>();
                break;
        }
    }
}


