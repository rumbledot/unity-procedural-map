using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterController : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnTransform;

    public static event Action PlayerReady;

    void Start()
    {
        Instantiate(Player, SpawnTransform.position, Quaternion.identity);

        PlayerReady?.Invoke
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
