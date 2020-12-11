using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeDigitSprite : MonoBehaviour
{

    public Sprite d0;
    public Sprite d1;
    public Sprite d2;
    public Sprite d3;
    public Sprite d4;
    public Sprite d5;
    public Sprite d6;
    public Sprite d7;
    public Sprite d8;
    public Sprite d9;
    public Sprite empty;

    public Image thisImage;

    public void ChangeSprite(float digit)
    {
        int d = (int)digit;
        switch (d)
        {
            case -1:
                thisImage.overrideSprite = empty;
                break;
            case 0:
                thisImage.overrideSprite = d0;
                break;
            case 1:
                thisImage.overrideSprite = d1;
                break;
            case 2:
                thisImage.overrideSprite = d2;
                break;
            case 3:
                thisImage.overrideSprite = d3;
                break;
            case 4:
                thisImage.overrideSprite = d4;
                break;
            case 5:
                thisImage.overrideSprite = d5;
                break;
            case 6:
                thisImage.overrideSprite = d6;
                break;
            case 7:
                thisImage.overrideSprite = d7;
                break;
            case 8:
                thisImage.overrideSprite = d8;
                break;
            case 9:
                thisImage.overrideSprite = d9;
                break;
            default:
                thisImage.overrideSprite = empty;
                break;
        }

    }
}
