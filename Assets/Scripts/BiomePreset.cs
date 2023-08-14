using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class BiomePreset : ScriptableObject
{
    public Material[] tiles;
    public GameObject[] objects;
    public float minHeight;
    public float minMoisture;
    public float minHeat;

    public Material GetTileMaterial()
    {
        return tiles[Random.Range(0, tiles.Length)];
    }

    public bool HasObjectsToSpawn()
    {
        return objects != null && objects.Length > 0;
    }

    public GameObject GetObjectToSpawn()
    {
        var pickAnObject = Random.Range(0, objects.Length + 1);

        if (pickAnObject >= objects.Length)
        {
            return null;
        }

        return objects[pickAnObject];
    }

    public bool MatchCondition(float height, float moisture, float heat)
    {
        return height >= minHeight && moisture >= minMoisture && heat >= minHeat;
    }
}
