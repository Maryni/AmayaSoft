using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AllData", menuName = "Data/All Data", order = 9)]
public class AllDatasBundle : ScriptableObject
{
    #region private variables

    [SerializeField]
    private CardBundleData[] cardDatas;

    [SerializeField]
    private SpawnerSettings[] spawnerDatas;

    #endregion private variables

    #region public variables

    public CardBundleData[] CardDatas => cardDatas;
    public SpawnerSettings[] SpawnerDatas => spawnerDatas;

    #endregion public variables
}