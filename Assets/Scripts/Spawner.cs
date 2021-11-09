using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public void Spawn(GameObject objectForSpawn, Transform transformWhenSpawn, Sprite sprite, string name, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var temp = Instantiate(objectForSpawn, transformWhenSpawn);
            temp.name = name;
            temp.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Image>().sprite = sprite;
        }
    }
}