using UnityEngine;
using System.Collections;

public class CharacterDescriptions : MonoBehaviour {

    public string GetCharacterDescription(string character)
    {
        if (character.Equals("FireMage"))
        {
            return "Fire Mage\nPowerful fire mages are rare. They can handle the wildness and heat of the fire.";
        }
        if (character.Equals("FrostMage"))
        {
            return "Frost Mage\nHandling the frost and ice is an ancient knowledge. A deadly knowledge.";
        }

        return "Error";
    }
}
