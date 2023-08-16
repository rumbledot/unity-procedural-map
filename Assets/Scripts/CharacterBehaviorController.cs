using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterBehaviorController : MonoBehaviour
{
    public GameObject body;
    public GameObject statsUI;
    public Transform statsTransform;
    public CharacterStats stats;

    private int speedGauge;
    private int maxSpeedGauge = 100;
    private int speedGaugeModifier;
    public bool liveAndKicking;

    public event Action<CharacterBehaviorController> onCharacterTurnEvent;
    public event Action<string> onDisplayInfo;

    private void Start()
    {
        liveAndKicking = true;
    }

    private void Update()
    {
        if (CombatManager.currentCombatState != CombatStates.Waiting)
        {
            return;
        }

        if(!liveAndKicking) 
        {
            return;
        }

        //if (CombatManager.currentCharacterTurn == this)
        //{
        //    StartCoroutine(TakeAction());
        //}

        speedGauge += stats.speed + speedGaugeModifier;
        if(speedGauge > maxSpeedGauge) 
        {
            speedGauge = 0;
            onCharacterTurnEvent?.Invoke(this);
        }
    }

    private IEnumerator TakeAction()
    {
        onDisplayInfo?.Invoke($"{stats.name} turn!");
        yield return new WaitForSeconds(2);
    }
}
