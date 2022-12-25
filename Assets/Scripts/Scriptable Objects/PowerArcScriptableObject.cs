using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerArc", menuName = "ScriptableObjects/PowerArc")]
public class PowerArcScriptableObject : ScriptableObject
{
    public string arcName;
    public float attackDamage = 1;
    public float speedModifier = 0;
    public float damageModifier = 0;
    public float defenseModifier = 0;
    public enum type
    {
        water, earth, fire, wind, ice, lightning, nature 
    }
}
