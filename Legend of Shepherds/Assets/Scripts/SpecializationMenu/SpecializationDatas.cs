using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecializationDatas : MonoBehaviour {

    string H_1_1;
    string H_1_2;
    string H_1_4;
    string H_2_1;
    string H_2_2;
    string H_2_4;
    string H_3_1;
    string H_3_2;
    string H_3_4;
    string H_3_8;
    string H_3_16;
    string H_3_32;
    string H_4_1;
    string H_4_2;
    string H_4_4;
    string H_5_1;
    string H_5_2;
    string H_5_4;
    string H_5_8;

    string A_1_1;
    string A_1_2;
    string A_1_4;
    string A_2_1;
    string A_2_2;
    string A_2_4;
    string A_3_1;
    string A_3_2;
    string A_3_4;
    string A_3_8;
    string A_3_16;
    string A_3_32;
    string A_4_1;
    string A_4_2;
    string A_4_4;
    string A_5_1;
    string A_5_2;
    string A_5_4;
    string A_5_8;

    string S_1_1;
    string S_1_2;
    string S_1_4;
    string S_2_1;
    string S_2_2;
    string S_2_4;
    string S_3_1;
    string S_3_2;
    string S_3_4;
    string S_3_8;
    string S_3_16;
    string S_3_32;
    string S_4_1;
    string S_4_2;
    string S_4_4;
    string S_5_1;
    string S_5_2;
    string S_5_4;
    string S_5_8;

    public void Initialize()
    {
        H_1_1 = "Gives you a spirit power. Upon activating gives 30% damage reducation for 10 seconds. Cooldown: 45 seconds";
        H_1_2 = "Increases movement speed by 3/6/10%";
        H_1_4 = "Increases bonus health by 5/10/15%";
        H_2_1 = "Increases bonus health by 5/10/15%";
        H_2_2 = "Increases bonus health by 5/10/15%";
        H_2_4 = "Increases bonus health by 18%";
        H_3_1 = "Increases armor by 5/10/15%";
        H_3_2 = "Increases health regeneration by 5/10/15%";
        H_3_4 = "Increases health regeneration by 5/10/15%";
        H_3_8 = "Increases bonus health by 5/10/15%";
        H_3_16 = "Increases armor by 5/10/15%";
        H_3_32 = "Empowers your spirit power to also heals you 20% maximum health over 10 seconds.";
        H_4_1 = "Increases bonus health by 5/10/15%";
        H_4_2 = "Increases armor by 5/10/15%";
        H_4_4 = "Increases bonus health by 18%";
        H_5_1 = "Increases armor by 5/10/15%"   ;
        H_5_2 = "Increases health regeneration by 5/10/15%";
        H_5_4 = "Increases health regeneration by 5/10/15%";
        H_5_8 = "Increases armor by 18%";

        A_1_1 = "Gives you a spirit power. Upon activating increases AD and MD by 25% for 10 seconds. Cooldown: 45 seconds";
        A_1_2 = "Increases cooldown reducation rating by 3/6/10%";
        A_1_4 = "Increases attack speed by 3/6/10%";
        A_2_1 = "Increases magic damage by 3/6/10%";
        A_2_2 = "Increases magic power by 5/10/15%";
        A_2_4 = "Increases magic power by 18%";
        A_3_1 = "Increases attack damage by 3/6/10%";
        A_3_2 = "Increases critical chance by 3/6/10%";
        A_3_4 = "Increases magic damage by 3/6/10%";
        A_3_8 = "Increases attack speed by 3/6/10%";
        A_3_16 = "Increases critical chance by 3/6/10%";
        A_3_32 = "Empowers your spirit power to also heals you 5% of your damage for 10 seconds.";
        A_4_1 = "Increases attack power by 5/10/15%";
        A_4_2 = "Increases attack damage by 3/6/10%";
        A_4_4 = "Increases attack power by 18%";
        A_5_1 = "Increases cooldown reducation rating by 3/6/10%";
        A_5_2 = "Increases cooldown reducation rating by 3/6/10%";
        A_5_4 = "Increases magic power by 5/10/15%";
        A_5_8 = "Increases attack power by 18%";

        S_1_1 = "Gives you a spirit power. Upon activating heals you for 20% (+20% based on missing health) base health. Cooldown: 45 seconds";
        S_1_2 = "Increases attack speed by 3/6/10%";
        S_1_4 = "Increases cooldown reducation rating by 3/6/10%";
        S_2_1 = "Increases all main stats by 3/6/10%";
        S_2_2 = "Increases movement speed by 3/6/10%";
        S_2_4 = "Increases all main stats by 12%";
        S_3_1 = "Increases medallion power by 5/10/15%";
        S_3_2 = "Increases all main stats by 3/6/10%";
        S_3_4 = "Increases weapon skill damage by 5/10/15%";
        S_3_8 = "Increases all main stats by 3/6/10%";
        S_3_16 = "Increases movement speed by 3/6/10%";
        S_3_32 = "Empowers your spirit power to also gives you 20% movement speed for 10 seconds.";
        S_4_1 = "Decreases medallion power cooldown by 5/10/15 seconds";
        S_4_2 = "Increases medallion power by 5/10/15%";
        S_4_4 = "Increases magic power by 18%";
        S_5_1 = "Decreases weapon skill cooldown by 1/2/3 seconds";
        S_5_2 = "Increases weapon skill damage by 5/10/15%";
        S_5_4 = "Increases movement speed by 3/6/10%";
        S_5_8 = "Increases all main stats by 12%";
    }

    public string GetSpecBonuses(string name)
    {

        switch (name)
        {
            case "H_1_1": return H_1_1;
            case "H_1_2": return H_1_2;
            case "H_1_4": return H_1_4;
            case "H_2_1": return H_2_1;
            case "H_2_2": return H_2_2;
            case "H_2_4": return H_2_4;
            case "H_3_1": return H_3_1;
            case "H_3_2": return H_3_2;
            case "H_3_4": return H_3_4;
            case "H_3_8": return H_3_8;
            case "H_3_16": return H_3_16;
            case "H_3_32": return H_3_32;
            case "H_4_1": return H_4_1;
            case "H_4_2": return H_4_2;
            case "H_4_4": return H_4_4;
            case "H_5_1": return H_5_1;
            case "H_5_2": return H_5_2;
            case "H_5_4": return H_5_4;
            case "H_5_8": return H_5_8;

            case "A_1_1": return A_1_1;
            case "A_1_2": return A_1_2;
            case "A_1_4": return A_1_4;
            case "A_2_1": return A_2_1;
            case "A_2_2": return A_2_2;
            case "A_2_4": return A_2_4;
            case "A_3_1": return A_3_1;
            case "A_3_2": return A_3_2;
            case "A_3_4": return A_3_4;
            case "A_3_8": return A_3_8;
            case "A_3_16": return A_3_16;
            case "A_3_32": return A_3_32;
            case "A_4_1": return A_4_1;
            case "A_4_2": return A_4_2;
            case "A_4_4": return A_4_4;
            case "A_5_1": return A_5_1;
            case "A_5_2": return A_5_2;
            case "A_5_4": return A_5_4;
            case "A_5_8": return A_5_8;

            case "S_1_1": return S_1_1;
            case "S_1_2": return S_1_2;
            case "S_1_4": return S_1_4;
            case "S_2_1": return S_2_1;
            case "S_2_2": return S_2_2;
            case "S_2_4": return S_2_4;
            case "S_3_1": return S_3_1;
            case "S_3_2": return S_3_2;
            case "S_3_4": return S_3_4;
            case "S_3_8": return S_3_8;
            case "S_3_16": return S_3_16;
            case "S_3_32": return S_3_32;
            case "S_4_1": return S_4_1;
            case "S_4_2": return S_4_2;
            case "S_4_4": return S_4_4;
            case "S_5_1": return S_5_1;
            case "S_5_2": return S_5_2;
            case "S_5_4": return S_5_4;
            case "S_5_8": return S_5_8;

            default: return "";
        }
    }

}
