using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.Show<PopupMain>();
    }
}
