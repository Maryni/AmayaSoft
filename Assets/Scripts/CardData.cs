using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData
{
    #region private variables

    [SerializeField]
    private string id;

    [SerializeField]
    private Sprite sprite;

    #endregion private variables

    #region public variables

    public string Id => id;
    public Sprite Sprite => sprite;

    #endregion public variables
}