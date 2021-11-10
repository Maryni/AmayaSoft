using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region private variables

    private SpawnController spawnController;
    private TextController textController;

    [SerializeField]
    private AllDatasBundle allDatas;

    [SerializeField]
    private GameObject panelRestart;

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
        StartLevelAndChangeText();
    }

    #endregion Unity functions

    #region public void

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
        panelRestart.SetActive(false);
        StartLevelAndChangeText();
    }

    #endregion public void

    #region private void

    private void StartLevelAndChangeText()
    {
        spawnController.SetData(allDatas);
        spawnController.StartLevel();
        textController.ChangeText(spawnController.GetLastFavoriteItem().Id);
        spawnController.GetUnityActionForFavoriteItem(
                () => spawnController.ChangingLevel(),
                () => spawnController.Spawning(),
                () => spawnController.SetLocalActionsToSpawnedItem(),
                () => textController.ChangeText(spawnController.GetLastFavoriteItem().Id)
                );
        spawnController.GetUnityActionForFinishGame(
            () => panelRestart.SetActive(true)
            );
    }

    #endregion private void
}