using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class EventsReservationController : MonoBehaviour
{
    [SerializeField] private List<MyEvents> _events;
    public Dictionary<string, MyEvents> events;    


    [System.Serializable] public class MyEvents
    {
        public string name;
        public float waitTime = 0f;
        public float _waitTime { set { waitTime = value; } }

        [Space(20)]
        public UnityEvent unityEvent;
    }

    void Start()
    {
        foreach (MyEvents e in _events)
        {
            events.Add(e.name, e);
        }
    }

    public void Invoke(string name)
    {
        StartCoroutine(Delay(name));
    }

    IEnumerator Delay(string name)
    {
        if (events[name].waitTime != 0)
            yield return new WaitForSeconds(events[name].waitTime);

        events[name].unityEvent.Invoke();
    }
}
