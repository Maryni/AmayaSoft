using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    #region private variables

    private UnityEvent unityEvent;

    #endregion private variables

    #region public void

    public void AddEvent(UnityAction action)
    {
        unityEvent.AddListener(action);
    }

    public void Click()
    {
        unityEvent.Invoke();
    }

    #endregion public void
}