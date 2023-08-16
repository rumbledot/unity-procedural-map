using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatStateController : MonoBehaviour
{
    public CharacterBehaviorController Player;
    public GameObject[] enemies;

    public TMP_Text Message;
    public Scrollbar PlayerHealthBar;
    public Scrollbar PlayerMagicBar;
    public Scrollbar PlayerPowerBar;

    private CharacterBehaviorController[] enemyControllers;
    private CharacterBehaviorController currentEnemy;
    private int choice;

    public static event Action PlayerReady;

    void Start()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyControllers = new CharacterBehaviorController[enemies.Length];

        Player.onCharacterTurnEvent += OnCharacterTurnEvent;

        int enemyIndex = 0;
        foreach (var enemy in enemies)
        {
            enemyControllers[enemyIndex] = enemy.GetComponent<CharacterBehaviorController>();
            Debug.Log(enemyControllers[enemyIndex]);
            enemyControllers[enemyIndex].onCharacterTurnEvent += OnCharacterTurnEvent;
            enemyControllers[enemyIndex].onDisplayInfo += OnCharacterDisplayInfo;
        }

        CombatManager.currentCombatState = CombatStates.Start;
    }

    private void OnDestroy()
    {
        Player.onCharacterTurnEvent -= OnCharacterTurnEvent;

        for (var i = 0; i < enemyControllers.Length; i++)
        {
            enemyControllers[i].onCharacterTurnEvent -= OnCharacterTurnEvent;
            enemyControllers[i].onDisplayInfo -= OnCharacterDisplayInfo;
        }
    }

    private void OnCharacterTurnEvent(CharacterBehaviorController character)
    {
        CombatManager.currentCharacterTurn = character;
        var stats = character.stats;
        Debug.Log($"CharacterTurn Event {stats.name}");
        if (stats.name.Equals("player", StringComparison.InvariantCultureIgnoreCase))
        {
            CombatManager.currentCombatState = CombatStates.PlayerTurn;
        }
        else 
        {
            CombatManager.currentCombatState = CombatStates.EnemyTurn;
        }

        Message.text = $"{character.name} turn!";
    }

    private void OnCharacterDisplayInfo(string info)
    {
        Message.text = info;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CombatManager.currentCombatState);
        if(CombatManager.currentCombatState == CombatStates.Start) 
        {
            StartCoroutine(CombatStart());
        }
        else if (CombatManager.currentCombatState == CombatStates.Waiting)
        {

        }
        else if (CombatManager.currentCombatState == CombatStates.PlayerTurn)
        {
            StartCoroutine(PlayerTurn());
        }
        else if (CombatManager.currentCombatState == CombatStates.EnemyTurn)
        {
            StartCoroutine(EnemyTurn());
        }
        else if (CombatManager.currentCombatState == CombatStates.Win)
        {
            Message.text = "Player Win";
        }
        else if (CombatManager.currentCombatState == CombatStates.Lost)
        {
            Message.text = "Player Dead";
        }
        else
        {
            Message.text = "Combat end!";
        }
    }

    IEnumerator CombatStart()
    {
        Message.text = $"Prepare youe self! {enemyControllers.Length} enemies approaching!";
        yield return new WaitForSeconds(2);
        //yield return StartCoroutine(WaitForKeyDown(new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1 }));
        Message.text = $"To Batte!";
        yield return new WaitForSeconds(2);

        CombatManager.currentCombatState = CombatStates.Waiting;
    }

    IEnumerator PlayerTurn()
    {
        Message.text = "Player TURN!";
        yield return new WaitForSeconds(6);
        CombatManager.currentCombatState = CombatStates.Waiting;
    }

    IEnumerator EnemyTurn()
    {
        Message.text = $"{CombatManager.currentCharacterTurn.stats.name} TURN!";
        yield return new WaitForSeconds(4);
        CombatManager.currentCombatState = CombatStates.Waiting;
    }

    IEnumerator WaitForKeyDown(KeyCode[] codes)
    {
        bool pressed = false;
        while (!pressed)
        {
            foreach (KeyCode k in codes)
            {
                if (Input.GetKey(k))
                {
                    pressed = true;
                    SetChoiceTo(k);
                    break;
                }
            }
            yield return null; //you might want to only do this check once per frame -> yield return new WaitForEndOfFrame();
        }
    }

    private void SetChoiceTo(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case (KeyCode.Alpha0):
                choice = 0;
                break;
            case (KeyCode.Alpha1):
                choice = 1;
                break;
        }
    }
}
