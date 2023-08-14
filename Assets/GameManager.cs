using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static CharacterStats PlayerStats;
    public static Vector3 PlayerPosition;
    public static Quaternion PlayerRotation;

    public static bool MapGenerated = false;
    private static GameScenes currentScene;
    public static BiomePreset BiomePreset;

    public static float[,] heightMap;
    public static float[,] moistureMap;
    public static float[,] heatMap;

    private void Awake()
    {
        Instance = this;

        if (currentScene == GameScenes.None)
        {
            currentScene = GameScenes.MapScene;
        }
    }

    public static void NavigateToScene(GameScenes state)
    {
        switch (state)
        {
            case GameScenes.MapScene:
                NavigateToMap();
                break;
            case GameScenes.EncounterScene:
                NavigateToEncounter();
                break;
            default:
                NavigateToDungeon();
                break;
        }
    }

    private static void NavigateToMap()
    {
        //SavePlayerStats();

        SceneManager.LoadScene(GameScenes.MapScene.ToString(), LoadSceneMode.Single);
    }

    private static void NavigateToEncounter()
    {
        SavePlayerStats();
        SavePlayerTransform();

        SceneManager.LoadScene(GameScenes.EncounterScene.ToString(), LoadSceneMode.Single);
    }

    private static void NavigateToDungeon()
    {
        SavePlayerStats();
        SavePlayerTransform();

        SceneManager.LoadScene(GameScenes.DungeonScene.ToString(), LoadSceneMode.Single);
    }

    private static void SavePlayerStats()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.GetComponent<PlayerCharacterBehavior>());
        PlayerStats = player.GetComponent<PlayerCharacterBehavior>().playerStats;
    }

    private static void SavePlayerTransform()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = player is null ? new Vector3(50f, 6f, 50f) : player.gameObject.transform.position;
        PlayerRotation = player is null ? Quaternion.identity : player.gameObject.transform.rotation;
    }

    public static Vector3 GetPlayerPosition()
    {
        if (PlayerPosition == null || PlayerPosition == Vector3.zero)
        {
            return new Vector3(50, 6, 50);
        }

        return PlayerPosition;
    }

    public static Quaternion GetPlayerRotation()
    {
        if (PlayerRotation == null)
        {
            return Quaternion.identity;
        }

        return PlayerRotation;
    }

    public static CharacterStats GetPlayerStats()
    {
        return PlayerStats;
    }
}