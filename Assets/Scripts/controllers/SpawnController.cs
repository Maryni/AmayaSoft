using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    #region private variables

    private AllDatasBundle allDatas;

    [Header("Spawned Object"), SerializeField]
    private Transform transformCube;

    [SerializeField]
    private GameObject cube;

    [Header("Other"), SerializeField]
    private Level currentLevel;

    [SerializeField]
    private List<CardData> listUsed;

    [SerializeField]
    private List<CardData> listCardDatas;

    private Spawner spawner;
    private int indexForTypeData;
    private int countNeedSpawnCurrentLevel;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (spawner == null)
        {
            spawner = GetComponent<Spawner>();
        }
    }

    #endregion Unity functions

    #region public void

    public void SetData(AllDatasBundle data)
    {
        allDatas = data;
    }

    public void SetLevel(Level level)
    {
        currentLevel = level;
        listCardDatas.Clear();
        indexForTypeData = Random.Range(0, allDatas.CardDatas.Length);
    }

    public void Spawning()
    {
        SetCurrentLevelUsingItems();
        Spawn(currentLevel);
    }

    public CardData GetLastFavoriteItem()
    {
        return listUsed.Last();
    }

    #endregion public void

    #region private void

    private void Spawn(Level level)
    {
        for (int i = 0; i < countNeedSpawnCurrentLevel; i++)
        {
            spawner.Spawn(cube, transformCube, listCardDatas[i].Sprite, listCardDatas[i].Id, 1);
        }
    }

    private void SetCurrentLevelUsingItems()
    {
        var currentSettingData = allDatas.SpawnerDatas.FirstOrDefault(x => x.LevelName == currentLevel);
        countNeedSpawnCurrentLevel = currentSettingData.LineCount * currentSettingData.ColumnCount;
        var currentLevelCardData = allDatas.CardDatas[indexForTypeData].CardData;
        for (int i = 0; i < countNeedSpawnCurrentLevel; i++)
        {
            SetRandomItemFromData(currentLevelCardData, listCardDatas);
        }
        SetFavoriteItem();
    }

    private void SetFavoriteItem()
    {
        SetRandomItemFromData(listCardDatas, listUsed);
    }

    private void SetRandomItemFromData(CardData[] dataInput, List<CardData> dataOutput)
    {
        var randomItem = dataInput[Random.Range(0, dataInput.Length)];
        if (!dataOutput.Contains(randomItem))
        {
            dataOutput.Add(randomItem);
        }
        else
        {
            do
            {
                randomItem = dataInput[Random.Range(0, dataInput.Length)];
            }
            while (dataOutput.Contains(randomItem));
            dataOutput.Add(randomItem);
        }
    }

    private void SetRandomItemFromData(List<CardData> dataInput, List<CardData> dataOutput)
    {
        var randomItem = dataInput[Random.Range(0, dataInput.Count)];
        if (!dataOutput.Contains(randomItem))
        {
            dataOutput.Add(randomItem);
        }
        else
        {
            do
            {
                randomItem = dataInput[Random.Range(0, dataInput.Count)];
            }
            while (dataOutput.Contains(randomItem));
            dataOutput.Add(randomItem);
        }
    }

    #endregion private void
}