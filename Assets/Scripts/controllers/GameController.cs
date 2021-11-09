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
        spawnController.SetLevel(Level.Low);
        spawnController.Spawning();
        textController.ChangeText(spawnController.GetLastFavoriteItem().Id);
    }

    #endregion Unity functions
}