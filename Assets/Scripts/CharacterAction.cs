using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Action", menuName = "Character/New Character Action")]
public class CharacterAction : ScriptableObject
{
    public string actionName;
    public int cost;
    public int minDamage;
    public int maxDamage;
    public ActionTargetTypes targetType;
}