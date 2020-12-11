using UnityEngine;
using System.Collections;

public class NewCharacterMenuImages : MonoBehaviour {

    public Sprite emptyCharacter;

    public Sprite fireMage_selected;
    public Sprite fireMage_deselected;
    public Sprite fireMage_skillSample_1;
    public Sprite fireMage_skillSample_2;
    public Sprite fireMage_skillSample_3;
    public Sprite fireMage_skillSample_4;

    public Sprite frostMage_selected;
    public Sprite frostMage_deselected;
    public Sprite frostMage_skillSample_1;
    public Sprite frostMage_skillSample_2;
    public Sprite frostMage_skillSample_3;
    public Sprite frostMage_skillSample_4;



    public Sprite GetSkillSampleSprite(int skillNumber, string character)
    {
        if (character.Equals("FireMage"))
        {
            switch (skillNumber)
            {
                case 1:
                    return fireMage_skillSample_1;
                case 2:
                    return fireMage_skillSample_2;
                case 3:
                    return fireMage_skillSample_3;
                case 4:
                    return fireMage_skillSample_4;
                default:
                    return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
            }
        }

        if (character.Equals("FrostMage"))
        {
            switch (skillNumber)
            {
                case 1:
                    return fireMage_skillSample_1;
                case 2:
                    return fireMage_skillSample_2;
                case 3:
                    return fireMage_skillSample_3;
                case 4:
                    return fireMage_skillSample_4;
                default:
                    return Sprite.Create(new Texture2D(0, 0), new Rect(), Vector2.zero);
            }
        }

        return emptyCharacter;
    }

    public Sprite GetCharacterSprite(bool selected, string character)
    {
        if (character.Equals("FireMage"))
        {
            if (selected)
                return fireMage_selected;
            else
                return fireMage_deselected;
        }

        if (character.Equals("FrostMage"))
        {
            if (selected)
                return frostMage_selected;
            else
                return frostMage_deselected;
        }

        return emptyCharacter;
    }


}
