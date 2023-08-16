using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;

    public static CombatStates currentCombatState;
    public static CharacterBehaviorController currentCharacterTurn;

    private void Awake()
    {
        Instance = this;
    }
}
