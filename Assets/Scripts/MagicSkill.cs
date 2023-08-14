using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magic Skill", menuName = "Character/New Magic Skill")]
public class MagicSkill : ScriptableObject
{
    public int cost;
    public int maxDamage;
    public int minDamage;
    public MagicalEffects magicalEffect;
    public int effectDamage;
    public int effectTimer;
}