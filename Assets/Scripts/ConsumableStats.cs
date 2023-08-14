using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable Stats", menuName = "Character/New Consumable")]
public class ConsumableStats : ScriptableObject
{
    public int power;
    public MagicalEffects[] magicalEffects;
    public BuffEffects[] effects;
}
