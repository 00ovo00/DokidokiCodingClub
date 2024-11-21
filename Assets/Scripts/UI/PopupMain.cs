using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMain : UIBase
{
    public void Showpopup(int index)
    {
        switch (index)
        {
            case 0:
                UIManager.Instance.Show<PopupA>();
                break;
            case 1:
                UIManager.Instance.Show<OptionPanel>();
                break;
            case 2:
                break;
            case 3:
                break;
        }
    }
}
