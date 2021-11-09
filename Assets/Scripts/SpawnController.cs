using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    #region private variables

    [Header("Data"), SerializeField]
    private AllDatasBundle allDatas;

    [Header("Spawned Object"), SerializeField]
    private Transform transformCube;

    [SerializeField]
    private GameObject cube;

    [Header("Other"), SerializeField]
    private Level currentLevel;

    [SerializeField]
    private List<int> listUsedIndex;

    private Spawner spawner;

    #endregion private variables

    #region Unity functions

    private void OnValidate()
    {
        if (spawner == null)
        {
            spawner = GetComponent<Spawner>();
        }
    }

    private void Start()
    {
        Spawning();
        //test
    }

    #endregion Unity functions

    #region public void

    public void Spawning()
    {
        Spawn(currentLevel);
    }

    private void Spawn(Level level)
    {
        spawner.GetObject(cube);
        spawner.GetTransform(transformCube);
        SpawnerSettings settings;
        if (level == Level.Low)
        {
            settings = allDatas.SpawnerDatas.FirstOrDefault(x => x.LevelName == Level.Low);
        }
        else if (level == Level.Medium)
        {
            settings = allDatas.SpawnerDatas.FirstOrDefault(x => x.LevelName == Level.Medium);
        }
        else
        {
            settings = allDatas.SpawnerDatas.FirstOrDefault(x => x.LevelName == Level.High);
        }
        spawner.Spawn(settings.ColumnCount * settings.LineCount);
    }

    #endregion public void
}