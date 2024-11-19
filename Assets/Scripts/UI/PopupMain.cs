using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMain : UIBase
{
    public void ShowPopup(int index)
    {
        if(index == 0)
        {
            UIManager.Instance.Show<WriteNameScene>();
        }
        else if (index == 1)
        {

        }
        else if(index == 2)
        {

        }
        else if(index == 3)
        {

        }
    }
}
