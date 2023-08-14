using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor Stats", menuName = "Character/New Armor Stats")]
public class ArmorStats : ScriptableObject
{
    public int health;
    public int strength;
}
