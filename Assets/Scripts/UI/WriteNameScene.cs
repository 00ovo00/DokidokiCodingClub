using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteNameScene : UIBase
{
    public override void Opened(params object[] param)
    {
        // 데이터를 주고 받을 일이 잇을 때 사용
        //base.Opened(param);
        //
        //string str = (string)param[0];
        //int num = (int)param[1];
    }

    public void ShowMain()
    {
        UIManager.Instance.Show<PopupMain>();
        Hide();
    }
}
