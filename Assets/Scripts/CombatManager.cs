using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public TMP_Text Message;
    public Scrollbar PlayerHealthBar;
    public Scrollbar PlayerMagicBar;
    public Scrollbar PlayerPowerBar;

    public GameObject[] enemies;

    private GameObject Player;

    public enum CombatStates
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        End
    }

    void Start()
    {
        EncounterController.PlayerReady += EncounterController_PlayerReady;
    }

    private void OnDestroy()
    {
        EncounterController.PlayerReady -= EncounterController_PlayerReady;
    }

    private void EncounterController_PlayerReady()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        var playerStats = GameManager.GetPlayerStats();
        Player.GetComponent<PlayerCharacterBehavior>().playerStats = playerStats;

        PlayerHealthBar.value = playerStats.health / playerStats.maxHealth;
        PlayerMagicBar.value = playerStats.magic / playerStats.maxMagic;
        PlayerPowerBar.value = playerStats.power / playerStats.maxPower;

        Message.text = "Three enemies ambush you..";

        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(2f);

        Message.text = "Your turn";

        yield return new WaitForSeconds(2f);

        Message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
