using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region private variables

    private GameObject objectForSpawn;
    private Transform transformWhenSpawn;

    #endregion private variables

    #region public void

    public void GetObject(GameObject gameObject)
    {
        objectForSpawn = gameObject;
    }

    public void GetTransform(Transform transform)
    {
        transformWhenSpawn = transform;
    }

    public void Spawn(int count)
    {
        if (objectForSpawn != null && transformWhenSpawn != null)
        {
            for (int i = 0; i < count; i++)
            {
                Instantiate(objectForSpawn, transformWhenSpawn);
            }
        }
        else
        {
            Debug.LogError("[Object for Spawn] or [Transform When Object will be spawned] is null");
        }
    }

    #endregion public void
}