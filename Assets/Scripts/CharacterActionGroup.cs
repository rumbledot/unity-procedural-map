using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action Group", menuName = "Character/New Action Group")]
public class CharacterActionGroup : ScriptableObject
{
    public string actionGroupName;
    public CharacterAction[] actions;
}