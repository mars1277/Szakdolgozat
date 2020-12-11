using UnityEngine;
using System.Collections;

public class Datas : MonoBehaviour {

    //Player attributes
    /* private float[] HealthPoints_Player = { 50f, 60f,   70f,   90f,   110f,  140f,  170f,  210f,  250f,  300f,  350f,       // Level 0-10
                                                  410f,  470f,  540f,  610f,  690f,  770f,  860f,  950f,  1050f, 1150f,      // Level 11-20
                                                  1270f, 1390f, 1530f, 1670f, 1830f, 1990f, 2170f, 2350f, 2550f, 2750f,      // Level 21-30
                                                  2970f, 3190f, 3430f, 3670f, 3930f, 4190f, 4470f, 4750f, 5050f, 5350f,      // Level 31-40
                                                  5680f, 6010f, 6370f, 6730f, 7120f, 7510f, 7930f, 8350f, 8800f, 9250f       // Level 41-50
                                         };*/
    private float[,] DayValues = { 
        { 1, 1, 1 }, 
        {11, 6, 1.1962f}, 
        {51, 36, 1.0458f}, 
        {121, 216, 1.0259f}, 
        {221, 1296, 1.0181f}, 
        {351, 7776, 1.0139f}
    };

    private int[] EnemyPoints = { 10, 20, 70, 145, 400, 99999999 };

            private float[] HealthPoints_Player = { 50f,    70f,     110f,   170f,   250f,   350f,       // Level 0-10
                                                           470f,   610f,   770f,   950f,  1150f,      // Level 11-20
                                                          1390f, 1670f,  1990f,  2350f,  2750f,      // Level 21-30
                                                          3190f,  3670f,  4190f, 4750f, 5350f,      // Level 31-40
                                                          6010f, 6730f,  7510f, 8350f, 9250f       // Level 41-50
                                        };
    private float[] AutoAttackDamage_Player = { 2f, 3f,   4f,   5f,   7f,   9f,   11f,  14f,  17f,  20f,  24f,              // Level 0-10
                                                    28f,  32f,  37f,  42f,  50f,  58f,  67f,  76f,  86f,  96f,              // Level 11-20
                                                    107f, 118f, 130f, 142f, 158f, 174f, 191f, 208f, 226f, 244f,             // Level 21-30
                                                    263f, 282f, 302f, 322f, 347f, 372f, 398f, 424f, 451f, 478f,             // Level 31-40
                                                    506f, 534f, 563f, 592f, 627f, 662f, 698f, 734f, 771f, 808f              // Level 41-50
                                        };
    private float[] AutoAttackSpeed_Player = { 1.5f, 1.55f, 1.6f, 1.65f, 1.7f, 1.75f, 1.8f, 1.85f, 1.9f, 1.95f, 2f,         // Level 0-10
                                                     2.05f, 2.1f, 2.15f, 2.2f, 2.25f, 2.3f, 2.35f, 2.4f, 2.45f, 2.5f,       // Level 11-20
                                                     2.55f, 2.6f, 2.65f, 2.7f, 2.75f, 2.8f, 2.85f, 2.9f, 2.95f, 3f,         // Level 21-30
                                                     3.05f, 3.1f, 3.15f, 3.2f, 3.25f, 3.3f, 3.35f, 3.4f, 3.45f, 3.5f,       // Level 31-40
                                                     3.55f, 3.6f, 3.65f, 3.7f, 3.75f, 3.8f, 3.85f, 3.9f, 3.95f, 4f          // Level 41-50
                                        };

     private int[] ExperiencePoints_Player = {  500,    800,    1000,   1200,   1400,   1600,   1800,   2000,   2200,   4400,
                                                5000,   5600,   6200,   6800,   7400,   8000,   8600,   9200,   9800,   19600,
                                                21400,  23200,  25000,  26800,  28600,  30400,  32200,  34000,  35800,  71600,
                                                77000,  82400,  87800,  93200,  98600,  104000, 109400, 114800, 120200, 240400,
                                                256600, 272800, 289000, 305200, 321400, 337600, 353800, 370000, 386200, 772400
                                        };

    private float[] DurabityPoints_Wall = { 50f, 100f,   150f,   200f,   300f,   400f,   500f,   600f,   700f,   800f,   1000f,       // Level 0-10
                                                 1500f,  2000f,  3000f,  4000f,  6000f,  8000f,  10000f, 12000f, 13000f, 15000f,      // Level 11-20
                                                 17000f, 20000f, 23000f, 26000f, 30000f, 34000f, 38000f, 42000f, 46000f, 50000f,
                                                 55000f, 60000f, 65000f, 70000f, 75000f, 80000f, 85000f, 90000f, 95000f, 100000f,
                                                 110000f, 120000f, 130000f, 140000f, 150000f, 160000f, 170000f, 180000f, 190000f, 200000f
                                        };

    private float[] AdvancementValues =
    {
        0.8f,   1f,   1.2f,   1.5f,  1.9f,   2.4f,      3f,    3.7f,  4.5f,     5.4f,   7f,                         // Level 0-10
             8.05f,  9.45f,  11.2f, 13.3f, 15.75f,  18.55f,   21.7f, 25.2f,  29.05f,   35f,                         // Level 11-20
            40.25f, 47.25f,    56f, 66.5f, 78.75f,  92.75f,  108.5f,  126f, 145.25f,  175f,                         // Level 21-30
            192.5f,   217f, 248.5f,  287f, 332.5f,    385f,  444.5f,  511f,  584.5f,  700f,                         // Level 31-40
              742f, 801.5f, 878.5f,  973f,  1085f, 1214.5f, 1361.5f, 1526f,   1708f, 2100f                          // Level 41-50

    };

    //Level 1 skills

    private float[] FireBallDamage_Player = { 0.3f,30f, 32f,   48f,   64f,   80f,   96f,   104f,  112f,  120f,  128f,  136f,    // Level 0-10
                                                    160f,  184f,  208f,  232f,  256f,  272f,  288f,  304f,  320f,  336f,    // Level 11-20
                                                    368f,  400f,  432f,  464f,  496f,  528f,  560f,  592f,  624f,  656f,    // Level 21-30
                                                    704f,  752f,  800f,  848f,  896f,  944f,  992f,  1040f, 1088f, 1136f,   // Level 31-40
                                                    1200f, 1264f, 1328f, 1392f, 1456f, 1536f, 1560f, 1584f, 1608f, 1632f    // Level 41-50    
                                        };

    private float[] FireBreathDamage_Player = { 0.2f,22f, 13f,  20f,  26f,  32f,  39f,  42f,  45f,  48f,  52f,  55f,              // Level 0-10
                                                    64f,  74f,  84f,  93f,  103f, 109f, 116f, 122f, 128f, 135f,             // Level 11-20
                                                    148f, 160f, 173f, 186f, 199f, 212f, 224f, 237f, 250f, 263f,             // Level 21-30
                                                    282f, 301f, 320f, 340f, 359f, 378f, 397f, 416f, 436f, 455f,             // Level 31-40
                                                    480f, 506f, 532f, 557f, 583f, 615f, 647f, 679f, 711f, 743f              // Level 41-50    
                                        };

    //Level 2 skills

    private float[] FireNovaDamage_Player = { 0.3f,24f, 0f,   0f,   0f,   0f,   52f,   58f,   64f,   70f,    76f,    82f,              // Level 0-10
                                                  96f,  111f, 125f, 140f, 154f, 164f, 173f, 183f,  192f,  202f,             // Level 11-20
                                                  221f, 240f, 260f, 279f, 298f, 317f, 336f, 356f,  375f,  394f,             // Level 21-30
                                                  423f, 452f, 480f, 509f, 638f, 567f, 596f, 624f,  653f,  682f,             // Level 31-40
                                                  720f, 759f, 797f, 836f, 874f, 922f, 970f, 1018f, 1066f, 1114f             // Level 41-50    
                                        };

    private float[] FireWallDamage_Player = { 0.2f,25f, 0f,   0f,   0f,   0f,   52f,   58f,   64f,   70f,    76f,    82f,              // Level 0-10
                                                  96f,  111f, 125f, 140f, 154f, 164f, 173f, 183f,  192f,  202f,             // Level 11-20
                                                  221f, 240f, 260f, 279f, 298f, 317f, 336f, 356f,  375f,  394f,             // Level 21-30
                                                  423f, 452f, 480f, 509f, 638f, 567f, 596f, 624f,  653f,  682f,             // Level 31-40
                                                  720f, 759f, 797f, 836f, 874f, 922f, 970f, 1018f, 1066f, 1114f             // Level 41-50    
                                        };

    //Level 3 skills

    private float[] FireMineDamage_Player = { 0.4f,40f, 0f,    0f,    0f,    0f,    0f,    0f,    0f,    0f,    0f,    136f,      // Level 0-10
                                                  160f,25f,  184f,  208f,  232f,  256f,  272f,  288f,  304f,  320f,  336f,      // Level 11-20
                                                  368f,  400f,  432f,  464f,  496f,  528f,  560f,  592f,  624f,  656f,      // Level 21-30
                                                  704f,  752f,  800f,  848f,  896f,  944f,  992f,  1040f, 1088f, 1136f,     // Level 31-40
                                                  1200f, 1264f, 1328f, 1392f, 1456f, 1536f, 1616f, 1696f, 1776f, 1856f      // Level 41-50    
                                        };

    private float[] MeteoriteDamage_Player = { 0.4f, 25f, 0f,    0f,    0f,    0f,    0f,    0f,    0f,    0f,    0f,    136f,      // Level 0-10
                                                  160f,  184f,  208f,  232f,  256f,  272f,  288f,  304f,  320f,  336f,      // Level 11-20
                                                  368f,  400f,  432f,  464f,  496f,  528f,  560f,  592f,  624f,  656f,      // Level 21-30
                                                  704f,  752f,  800f,  848f,  896f,  944f,  992f,  1040f, 1088f, 1136f,     // Level 31-40
                                                  1200f, 1264f, 1328f, 1392f, 1456f, 1536f, 1616f, 1696f, 1776f, 1856f      // Level 41-50    
                                        };

    //Level 4 skills

    private float[] FireRainDamage_Player = { 0.15f,22f, 13f,  20f,  26f,  32f,  39f,  42f,  45f,  48f,  52f,  55f,                // Level 0-10
                                                  64f,  74f,  84f,  93f,  103f, 109f, 116f, 122f, 128f, 135f,               // Level 11-20
                                                  148f, 160f, 173f, 186f, 199f, 212f, 224f, 237f, 250f, 263f,               // Level 21-30
                                                  282f, 301f, 320f, 340f, 359f, 378f, 397f, 416f, 436f, 455f,               // Level 31-40
                                                  480f, 506f, 532f, 557f, 583f, 615f, 647f, 679f, 711f, 743f                // Level 41-50    
                                        };

    private float[] FireElementalnDamage_Player = { 0.15f,15f, 13f,  20f,  26f,  32f,  39f,  42f,  45f,  48f,  52f,  55f,                // Level 0-10
                                                  64f,  74f,  84f,  93f,  103f, 109f, 116f, 122f, 128f, 135f,               // Level 11-20
                                                  148f, 160f, 173f, 186f, 199f, 212f, 224f, 237f, 250f, 263f,               // Level 21-30
                                                  282f, 301f, 320f, 340f, 359f, 378f, 397f, 416f, 436f, 455f,               // Level 31-40
                                                  480f, 506f, 532f, 557f, 583f, 615f, 647f, 679f, 711f, 743f                // Level 41-50    
                                        };


    public float Get_DayValues(int num, int type)
    {
        return DayValues[num, type];
    }

    public int Get_EnemyPoints(int num)
    {
        return EnemyPoints[num];
    }

    public float Get_HealthPoints_Player(int level)
    {
        return HealthPoints_Player[level];
    }

    public float Get_AutoAttackDamage_Player(int level)
    {
        return AutoAttackDamage_Player[level];
    }

    public float Get_AutoAttackSpeed_Player(int level)
    {
        return AutoAttackSpeed_Player[level];
    }

    public int Get_ExperiencePoints_Player(int level)
    {
        return ExperiencePoints_Player[level];
    }

    public float Get_DurabityPoints_Wall(int level)
    {
        return DurabityPoints_Wall[level];
    }

    public float Get_AdvancementValues(int level)
    {
        return AdvancementValues[level];
    }

    public float Get_FireBallDamage_Player(int level)
    {
        return FireBallDamage_Player[level];
    }

    public float Get_FireBreathDamage_Player(int level)
    {
        return FireBreathDamage_Player[level];
    }

    public float Get_FireNovaDamage_Player(int level)
    {
        return FireNovaDamage_Player[level];
    }

    public float Get_FireWallDamage_Player(int level)
    {
        return FireWallDamage_Player[level];
    }

    public float Get_FireMineDamage_Player(int level)
    {
        return FireMineDamage_Player[level];
    }

    public float Get_MeteoriteDamage_Player(int level)
    {
        return MeteoriteDamage_Player[level];
    }

    public float Get_FireRainDamage_Player(int level)
    {
        return FireRainDamage_Player[level];
    }

    public float Get_FireElementalDamage_Player(int level)
    {
        return FireElementalnDamage_Player[level];
    }

    float[] HealthPoints_Goblin_Melee = { 3f,   4f,   5f,   7f,   9f,   11f,  14f,  17f,  20f,  24f,              // Level 0-10
                                                    28f,  32f,  37f,  42f,  50f,  58f,  67f,  76f,  86f,  96f,              // Level 11-20
                                                    107f, 118f, 130f, 142f, 158f, 174f, 191f, 208f, 226f, 244f };

    float Speed_Goblin_Melee = 2;
    string Speed_Goblin_Melee_String = "Slow";

    public float Get_HealthPoints_Goblin_Melee(int level)
    {
        return HealthPoints_Goblin_Melee[level];
    }

    public float Get_Speed_Goblin_Melee()
    {
        return Speed_Goblin_Melee;
    }

    public string Get_Speed_Goblin_Melee_String()
    {
        return Speed_Goblin_Melee_String;
    }

    float[] HealthPoints_Ork = { 20f, 40f, 40f, 60f, 60f, 80f, 80f, 100f, 100f, 120f, 120f, 140f, 140f, 160f, 160f, 180f, 180f, 200f, 200f, 220f, 220f };
    float Speed_Ork = 2;
    string Speed_Ork_String = "Slow";

    public float Get_HealthPoints_Ork(int level)
    {
        return HealthPoints_Ork[level];
    }

    public float Get_Speed_Ork()
    {
        return Speed_Ork;
    }

    public string Get_Speed_Ork_String()
    {
        return Speed_Ork_String;
    }
}
