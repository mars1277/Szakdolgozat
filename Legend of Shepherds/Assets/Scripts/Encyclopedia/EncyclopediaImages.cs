using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaImages : MonoBehaviour {

    public Sprite Unknown_Sprite;

    public Sprite Goblin_Level1_Sprite;
    public Sprite Goblin_Level2_Sprite;
    public Sprite Goblin_Level3_Sprite;
    public Sprite Goblin_Level4_Sprite;
    public Sprite Goblin_Level5_Sprite;

    public Sprite Archer_Level1_Sprite;
    public Sprite Archer_Level2_Sprite;
    public Sprite Archer_Level3_Sprite;
    public Sprite Archer_Level4_Sprite;
    public Sprite Archer_Level5_Sprite;

    public Sprite Zombie_Level1_Sprite;
    public Sprite Zombie_Level2_Sprite;
    public Sprite Zombie_Level3_Sprite;
    public Sprite Zombie_Level4_Sprite;

    public Sprite Bat_Level1_Sprite;
    public Sprite Bat_Level2_Sprite;
    public Sprite Bat_Level3_Sprite;
    public Sprite Bat_Level4_Sprite;

    public Sprite SkeletonMage_Level1_Sprite;
    public Sprite SkeletonMage_Level2_Sprite;
    public Sprite SkeletonMage_Level3_Sprite;

    public Sprite SkeletonMageBoss_Sprite;

    public Sprite GetEnemySprite(string name, int level)
    {
        switch (name)
        {
            case "Unknown":
                return Unknown_Sprite;
            case "Goblin":
                switch (level)
                {
                    case 1:
                        return Goblin_Level1_Sprite;
                    case 2:
                        return Goblin_Level2_Sprite;
                    case 3:
                        return Goblin_Level3_Sprite;
                    case 4:
                        return Goblin_Level4_Sprite;
                    case 5:
                        return Goblin_Level5_Sprite;
                }
                break;
            case "Archer":
                switch (level)
                {
                    case 1:
                        return Archer_Level1_Sprite;
                    case 2:
                        return Archer_Level2_Sprite;
                    case 3:
                        return Archer_Level3_Sprite;
                    case 4:
                        return Archer_Level4_Sprite;
                    case 5:
                        return Archer_Level5_Sprite;
                }
                break;
            case "Zombie":
                switch (level)
                {
                    case 1:
                        return Zombie_Level1_Sprite;
                    case 2:
                        return Zombie_Level2_Sprite;
                    case 3:
                        return Zombie_Level3_Sprite;
                    case 4:
                        return Zombie_Level4_Sprite;
                }
                break;
            case "Bat":
                switch (level)
                {
                    case 1:
                        return Bat_Level1_Sprite;
                    case 2:
                        return Bat_Level2_Sprite;
                    case 3:
                        return Bat_Level3_Sprite;
                    case 4:
                        return Bat_Level4_Sprite;
                }
                break;
            case "SkeletonMage":
                switch (level)
                {
                    case 1:
                        return SkeletonMage_Level1_Sprite;
                    case 2:
                        return SkeletonMage_Level2_Sprite;
                    case 3:
                        return SkeletonMage_Level3_Sprite;

                }
                break;
        }
        return Unknown_Sprite;
    }

    public Sprite GetBossSprite(string name)
    {
        switch (name)
        {
            case "SkeletonMageBoss":
                return Unknown_Sprite;
        }
        return Unknown_Sprite;
    }
}
