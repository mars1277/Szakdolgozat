using UnityEngine;
using System.Collections;

public class Active_Dragable_Margin_Spirtes : MonoBehaviour {

    public Sprite fireWallMargin;
    public Sprite meteoriteMargin;


    public Sprite GetSkillMargin(int skillLevel, int skillNumber)
    {
        int slotNumber = PlayerPrefs.GetInt("GameSlot");
        string character = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");

        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillMargin(skillLevel, skillNumber);
        }
        return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
    }

    public Sprite GetFireMageSkillMargin(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 2:
                if (skillNumber == 2)
                    return fireWallMargin;
                else
                    return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
            case 3:
                if (skillNumber == 2)
                    return meteoriteMargin;
                else
                    return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
            default:
                return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
        }
    }

    public Vector2 GetSkillMarginSize(int skillLevel, int skillNumber)
    {
        int slotNumber = PlayerPrefs.GetInt("GameSlot");
        string character = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");

        if (character.Equals("FireMage"))
        {
            return GetFireMageSkillMarginSize(skillLevel, skillNumber);
        }
        return Vector2.zero;
    }

    public Vector2 GetFireMageSkillMarginSize(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 2:
                if (skillNumber == 2)
                    return new Vector3(348f, 132f);
                return Vector2.zero;
            case 3:
                if (skillNumber == 2)
                    return new Vector3(352f, 343f);
                return Vector2.zero;
            default:
                return Vector2.zero;
        }
    }
}
