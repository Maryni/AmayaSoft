using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;

public class SpawnController : MonoBehaviour
{
    #region private variables

    private AllDatasBundle allDatas;

    [Header("Spawned Object"), SerializeField]
    private Transform transformCubeActivePanel;

    [SerializeField]
    private Transform transformCubeHiddenPanel;

    [SerializeField]
    private GameObject cube;

    [Header("Other"), SerializeField]
    private Level currentLevel;

    [SerializeField]
    private List<CardData> listFavoriteItems = new List<CardData>();

    [SerializeField]
    private List<CardData> listCardDatasSpawned = new List<CardData>();

    [SerializeField]
    private List<GameObject> listGameobjectsSpawned = new List<GameObject>();

    private Spawner spawner;
    private int indexForTypeData;
    private int countNeedSpawnCurrentLevel;
    private Coroutine setActionsCoroutine;
    private UnityAction unityActionFavoriteItem;
    private UnityAction unityActionFinishGame;

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

    public void StartLevel()
    {
        SetLevel(Level.Low);
        SetLocalActionsToSpawnedItem();
        Spawning();
    }

    public void SetData(AllDatasBundle data)
    {
        allDatas = data;
    }

    public void Spawning()
    {
        SetCurrentLevelUsingItems();
        Spawn(currentLevel);
    }

    public CardData GetLastFavoriteItem()
    {
        return listFavoriteItems.Last();
    }

    public void SetLocalActionsToSpawnedItem()
    {
        if (setActionsCoroutine == null)
        {
            setActionsCoroutine = StartCoroutine(SetActionsToSpawnedItems());
        }
    }

    public void GetUnityActionForFavoriteItem(params UnityAction[] unityAction)
    {
        for (int i = 0; i < unityAction.Length; i++)
        {
            unityActionFavoriteItem += unityAction[i];
        }
    }

    public void GetUnityActionForFinishGame(params UnityAction[] unityAction)
    {
        for (int i = 0; i < unityAction.Length; i++)
        {
            unityActionFinishGame += unityAction[i];
        }
    }

    public void ChangingLevel()
    {
        if (currentLevel == Level.High)
        {
            unityActionFinishGame?.Invoke();
            StopCoroutine(setActionsCoroutine);
            setActionsCoroutine = null;
        }
        if (currentLevel == Level.Medium)
        {
            SetLevel(Level.High);
            RemoveAllChilds();
        }
        if (currentLevel == Level.Low)
        {
            SetLevel(Level.Medium);
            RemoveAllChilds();
        }
    }

    public void BounceEffectForSpawnedItems()
    {
        for (int i = 0; i < listGameobjectsSpawned.Count; i++)
        {
            listGameobjectsSpawned[i].transform.DOShakePosition(1.5f, 6, 4);
        }
    }

    #endregion public void

    #region private void

    private void RemoveAllChilds()
    {
        for (int i = listGameobjectsSpawned.Count - 1; i >= 0; i--)
        {
            listGameobjectsSpawned[i].transform.SetParent(transformCubeHiddenPanel);
            listGameobjectsSpawned.RemoveAt(i);
        }
    }

    private void SetLevel(Level level)
    {
        currentLevel = level;
        listCardDatasSpawned.Clear();
        indexForTypeData = Random.Range(0, allDatas.CardDatas.Length);
    }

    private IEnumerator SetActionsToSpawnedItems()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < listCardDatasSpawned.Count; i++)
        {
            for (int j = 0; j < listFavoriteItems.Count; j++)
            {
                var tempGameObject = transformCubeActivePanel.GetChild(i).gameObject;
                if (!listGameobjectsSpawned.Contains(tempGameObject))
                {
                    listGameobjectsSpawned.Add(tempGameObject);
                }
                if (listGameobjectsSpawned[i].GetComponent<Item>().CardData.Id != listFavoriteItems[j].Id)
                {
                    tempGameObject.GetComponent<Item>().RemoveEvents();
                    tempGameObject.GetComponent<Item>().AddEvent(() => tempGameObject.transform.GetChild(0).transform.GetChild(0).transform.DOShakeScale(2f, 0.25f, 2));
                }
                else
                {
                    tempGameObject.GetComponent<Item>().RemoveEvents();
                    //tempGameObject.GetComponent<Item>().AddEvent(() => StartFavoritItemCoroutine());
                    tempGameObject.GetComponent<Item>().AddEvent(() => tempGameObject.transform.GetChild(0).transform.GetChild(0).transform.DOShakePosition(5, 3, 7));
                    tempGameObject.GetComponent<Item>().AddEvent(unityActionFavoriteItem);
                }
            }
        }
        BounceEffectForSpawnedItems();
        setActionsCoroutine = null;
    }

    private void Spawn(Level level)
    {
        for (int i = 0; i < countNeedSpawnCurrentLevel; i++)
        {
            spawner.Spawn(cube, transformCubeActivePanel, listCardDatasSpawned[i].Sprite, listCardDatasSpawned[i].Id, 1);
        }
    }

    private void SetCurrentLevelUsingItems()
    {
        var currentSettingData = allDatas.SpawnerDatas.FirstOrDefault(x => x.LevelName == currentLevel);
        countNeedSpawnCurrentLevel = currentSettingData.LineCount * currentSettingData.ColumnCount;
        var currentLevelCardData = allDatas.CardDatas[indexForTypeData].CardData;
        for (int i = 0; i < countNeedSpawnCurrentLevel; i++)
        {
            SetRandomItemFromData(currentLevelCardData, listCardDatasSpawned);
        }
        SetFavoriteItem();
    }

    private void SetFavoriteItem()
    {
        SetRandomItemFromData(listCardDatasSpawned, listFavoriteItems);
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