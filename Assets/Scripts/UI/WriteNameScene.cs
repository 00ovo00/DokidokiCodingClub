using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteNameScene : UIBase
{
    public override void Opened(params object[] param)
    {
        // �����͸� �ְ� ���� ���� ���� �� ���
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
