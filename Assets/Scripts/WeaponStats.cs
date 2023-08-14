using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

[CreateAssetMenu(fileName = "Weapon Stats", menuName = "Character/New Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    public int baseDamage;
    public int minStrength;
    public MagicalEffects[] magicalEffects;
    public MeleeEffects[] meleeEffects;
}
