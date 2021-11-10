using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region private variables

    private SpawnController spawnController;
    private TextController textController;

    [SerializeField]
    private AllDatasBundle allDatas;

    [SerializeField]
    private Transform parentCubes;

    private CardData takenCardData;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (textController == null)
        {
            textController = GetComponent<TextController>();
        }
        if (spawnController == null)
        {
            spawnController = GetComponent<SpawnController>();
        }
    }

    private void Start()
    {
        spawnController.SetData(allDatas);
        spawnController.StartLevel();
        spawnController.Spawning();
        textController.ChangeText(spawnController.GetLastFavoriteItem().Id);
        spawnController.GetUnityActionForFavoriteItem(
            () => spawnController.ChangingLevel(),
            () => spawnController.Spawning(),
            () => spawnController.SetLocalActionsToSpawnedItem(),
            () => textController.ChangeText(spawnController.GetLastFavoriteItem().Id)
            );
    }

    #endregion Unity functions

    #region public void

    public void Clicked(CardData cardData)
    {
        takenCardData = cardData;
    }

    #endregion public void
}