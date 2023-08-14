using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee Skill", menuName = "Character/New Melee Skill")]
public class MeleeSkill : ScriptableObject
{
    public int cost;
    public int minDamage;
    public int maxDamage;
    public MeleeEffects meleeEffect;
}
