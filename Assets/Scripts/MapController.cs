using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class MapController : MonoBehaviour
{
    public BiomePreset[] biomes;
    public GameObject tilePrefab;
    public GameObject Player;

    public Slider healthBar;
    public Slider magicBar;
    public Slider powerBar;

    [Header("Dimensions")]
    public int width = 50;
    public int height = 50;
    public float scale = 1.0f;
    public Vector2 offset;
    public int heightModifier = 10;

    [Header("Height Map")]
    public MapWave[] heightWaves;

    [Header("Moisture Map")]
    public MapWave[] moistureWaves;

    [Header("Heat Map")]
    public MapWave[] heatWaves;

    private void Start()
    {
        if (!GameManager.MapGenerated)
        {
            InitializeMap();
        }

        RenderMap();
    }

    private void InitializeMap()
    {
        heightWaves = new MapWave[2]
        {
            new MapWave(){ seed=56, frequency = 0.05f, amplitude = 1 },
            new MapWave(){ seed=199.36f, frequency = 0.1f, amplitude = 0.5f }
        };

        moistureWaves = new MapWave[1]
        {
            new MapWave(){ seed=621, frequency = 0.03f, amplitude = 1 }
        };

        heatWaves = new MapWave[2]
        {
            new MapWave(){ seed=318.6f, frequency = 0.04f, amplitude = 1 },
            new MapWave(){ seed=329.7f, frequency = 0.02f, amplitude = 0.5f }
        };

        // height map
        GameManager.heightMap = NoiseGenerator.Generate(width, height, scale, heightWaves, offset);
        // moisture map
        GameManager.moistureMap = NoiseGenerator.Generate(width, height, scale, moistureWaves, offset);
        // heat map
        GameManager.heatMap = NoiseGenerator.Generate(width, height, scale, heatWaves, offset);

        GameManager.MapGenerated = true;
    }

    private void RenderMap() 
    {
        BiomePreset biome;
        GameObject tile;
        GameObject biomeObject;
        Transform spawnPosition;

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                tile = Instantiate(tilePrefab, new Vector3(x, GameManager.heightMap[x, y] * heightModifier, y), Quaternion.identity, this.transform);
                biome = GetBiome(GameManager.heightMap[x, y], GameManager.moistureMap[x, y], GameManager.heatMap[x, y]);
                tile.GetComponent<MeshRenderer>().material = biome.GetTileMaterial();

                if (biome.HasObjectsToSpawn())
                {
                    biomeObject = biome.GetObjectToSpawn();
                    if (biomeObject == null)
                    {
                        continue;
                    }

                    spawnPosition = tile.transform.GetChild(0).transform;
                    Instantiate(biomeObject, spawnPosition.position, Quaternion.identity, tile.transform);
                }
            }
        }

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(Player, GameManager.GetPlayerPosition(), GameManager.GetPlayerRotation());
        if (GameManager.PlayerStats == null)
        {
            var playerBaseStat = player.GetComponent<PlayerCharacterBehavior>().playerStats;
            GameManager.PlayerStats = playerBaseStat;
        }
        else 
        {
            player.GetComponent<PlayerCharacterBehavior>().playerStats = GameManager.PlayerStats;
        }

        healthBar.maxValue = GameManager.GetPlayerStats().maxHealth;
        healthBar.value = GameManager.GetPlayerStats().health;

        magicBar.maxValue = GameManager.GetPlayerStats().maxMagic;
        magicBar.value = GameManager.GetPlayerStats().magic;

        powerBar.maxValue = GameManager.GetPlayerStats().maxPower;
        powerBar.value = GameManager.GetPlayerStats().power;
    }

    private BiomePreset GetBiome(float height, float moisture, float heat)
    {
        List<BiomeTempData> biomeTemp = new List<BiomeTempData>();

        foreach (BiomePreset biome in biomes)
        {
            if (biome.MatchCondition(height, moisture, heat))
            {
                biomeTemp.Add(new BiomeTempData(biome));
            }
        }

        BiomePreset biomeToReturn = null;
        float curVal = 0.0f;
        foreach (BiomeTempData biome in biomeTemp)
        {
            if (biomeToReturn == null)
            {
                biomeToReturn = biome.biome;
                curVal = biome.GetDiffValue(height, moisture, heat);
            }
            else
            {
                if (biome.GetDiffValue(height, moisture, heat) < curVal)
                {
                    biomeToReturn = biome.biome;
                    curVal = biome.GetDiffValue(height, moisture, heat);
                }
            }
        }

        if (biomeToReturn == null)
            biomeToReturn = biomes[0];

        return biomeToReturn;
    }
}

public class BiomeTempData
{
    public BiomePreset biome;

    public BiomeTempData(BiomePreset preset)
    {
        biome = preset;
    }

    public float GetDiffValue(float height, float moisture, float heat)
    {
        return (height - biome.minHeight) + (moisture - biome.minMoisture) + (heat - biome.minHeat);
    }
}