using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 
/// </summary>
public class EventReservationController : MonoBehaviour 
{
    public float waitTime = 0f;
    public float _waitTime { set { waitTime = value; } }

    [Space(20)]
    public UnityEvent unityEvent;
    
    

    public void Invoke()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        if (waitTime != 0)
            yield return new WaitForSeconds(waitTime);

        unityEvent.Invoke();
    }
}
