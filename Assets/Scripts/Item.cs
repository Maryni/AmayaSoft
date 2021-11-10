using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    #region private variables

    private CardData cardData;
    private UnityEvent unityEvent = new UnityEvent();

    #endregion private variables

    #region properties

    public CardData CardData => cardData;

    #endregion properties

    #region Unity functions

    private void Start()
    {
        cardData = new CardData(name, GetComponent<Image>().sprite);
    }

    #endregion Unity functions

    #region public void

    public void RemoveEvents()
    {
        unityEvent.RemoveAllListeners();
    }

    public void AddEvent(UnityAction action)
    {
        unityEvent.AddListener(action);
    }

    public void Click()
    {
        unityEvent?.Invoke();
    }

    #endregion public void
}