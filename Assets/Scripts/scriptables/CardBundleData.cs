using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "Data/Card Bundle Data", order = 10)]
public class CardBundleData : ScriptableObject
{
    #region private variables

    [SerializeField]
    private CardData[] cardData;

    #endregion private variables

    #region public variables

    public CardData[] CardData => cardData;

    #endregion public variables
}