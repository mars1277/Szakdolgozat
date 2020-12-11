using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatType {

    public enum Stats { Health, AttackPower, MagicPower, Armor, HealthRegen, AttackSpeed, CritChance, CDReducation, MovementSpeed };

     public static string GetStatDescription(Stats stat)
    {
        switch (stat)
        {
            case Stats.Health:
                return "Health";
            case Stats.AttackPower:
                return "Attack Power";
            case Stats.MagicPower:
                return "Magic Power";
            case Stats.Armor:
                return "Armor";
            case Stats.HealthRegen:
                return "Health Regeneration";
            case Stats.AttackSpeed:
                return "Attack Speed";
            case Stats.CritChance:
                return "Critical Chance";
            case Stats.CDReducation:
                return "CD Reducation Rating";
            case Stats.MovementSpeed:
                return "Movement Speed";
            default:
                return "Something is wrong";
        }
    }
}
