using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Character/New Character Stats")]
public class CharacterStats : ScriptableObject
{
    public string name;
    public int level;
    public int experience;
    public int health;
    public int maxHealth;
    public int power;
    public int maxPower;
    public int magic;
    public int maxMagic;
    public int speed;
    public int strength;
    public int damage;
    public int baseArmor;
    public int inteligence;

    public CharacterActionGroup[] characterActions;
}
