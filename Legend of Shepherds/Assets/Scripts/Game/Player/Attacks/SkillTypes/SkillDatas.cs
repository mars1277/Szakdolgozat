using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillDatas : MonoBehaviour {

    UniversalSkillType ust;

    public Sprite firemage_1_1;
    public Sprite firemage_1_2;
    public Sprite firemage_2_1;
    public Sprite firemage_2_2;
    public Sprite firemage_3_1;
    public Sprite firemage_3_2;
    public Sprite firemage_4_1;
    public Sprite firemage_4_2;

    public UniversalSkillType GetSkillDatas(int skillLevel, int skillNumber)
    {
        int slotNumber = PlayerPrefs.GetInt("GameSlot");
        string character = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");

        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillDatas(skillLevel, skillNumber);
        }
        return ust = new UniversalSkillType(0, 0, "Error", 0, Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero));
    }

    public UniversalSkillType GetFireMageSkillDatas(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 0:
                return ust = new UniversalSkillType(0, 0, "Nothing", 0f, Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero));
            case 1:
                if (skillNumber == 1)
                    return ust = new UniversalSkillType(1, 1, "Active_Normal", 8f, firemage_1_1);
                else
                    return ust = new UniversalSkillType(1, 2, "Active_Normal", 12f, firemage_1_2);
            case 2:
                if (skillNumber == 1)
                    return ust = new UniversalSkillType(2, 1, "Active_Normal", 16f, firemage_2_1);
                else
                    return ust = new UniversalSkillType(2, 2, "Active_Dragable", 20f, GameObject.Find("Datas").GetComponent<Active_Dragable_Margin_Spirtes>().GetSkillMargin(2, 2), GameObject.Find("Datas").GetComponent<Active_Dragable_Margin_Spirtes>().GetSkillMarginSize(2, 2), firemage_2_2);
            case 3:
                if (skillNumber == 1)
                    return ust = new UniversalSkillType(3, 1, "Active_WithCharges", 16f, 3, firemage_3_1);
                else
                    return ust = new UniversalSkillType(3, 2, "Active_Dragable", 20f, GameObject.Find("Datas").GetComponent<Active_Dragable_Margin_Spirtes>().GetSkillMargin(3, 2), GameObject.Find("Datas").GetComponent<Active_Dragable_Margin_Spirtes>().GetSkillMarginSize(3, 2), firemage_3_2);
            case 4:
                if (skillNumber == 1)
                    return ust = new UniversalSkillType(4, 1, "Active_Normal", 60f, firemage_4_1);
                else
                    return ust = new UniversalSkillType(4, 2, "Active_Normal", 80f, firemage_4_2);
            default:
                return ust = new UniversalSkillType(0, 0, "Error", 0, Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero));
        }
    }



}
