using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    Low,
    Medium,
    High
}

[CreateAssetMenu(fileName = "New Spawn Settings", menuName = "Data/Spawn Settings", order = 11)]
public class SpawnerSettings : ScriptableObject
{
    #region private variables

    [SerializeField]
    private Level level;

    [SerializeField]
    private int columnCount;

    [SerializeField]
    private int lineCount;

    #endregion private variables

    #region public variables

    public Level LevelName => level;
    public int ColumnCount => columnCount;
    public int LineCount => lineCount;

    #endregion public variables
}