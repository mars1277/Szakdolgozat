using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour {

    GameObject attack;


    public GameObject UseSkill(int skillLevel, int skillNumber)
    {
        int slotNumber = PlayerPrefs.GetInt("GameSlot");
        string character = PlayerPrefs.GetString("Slot" + slotNumber + "_Character");

        if (character.Equals("FireMage"))
        {
            return UseFireMageSkill(skillLevel, skillNumber);
        }
        if (character.Equals("FrostMage"))
        {
            return UseFrostMageSkill(skillLevel, skillNumber);
        }

        return new GameObject();
    }

    public GameObject UseFireMageSkill(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 0:
                attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageAttack);
                break;
            case 1:
                if (skillNumber == 1) 
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_1_1);            
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_1_2);
                break;
            case 2:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_2_1);
                else            
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_2_2);             
                break;
            case 3:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_3_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_3_2);
                break;
            case 4:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_4_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_4_2);
                break;
            default:
                break;
        }
        return attack;
    }

    public GameObject UseFrostMageSkill(int skillLevel, int skillNumber)
    {
        switch (skillLevel)
        {
            case 0:
                attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageAttack);
                break;
            case 1:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_1_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_1_2);
                break;
            case 2:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_2_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_2_2);
                break;
            case 3:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_3_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_3_2);
                break;
            case 4:
                if (skillNumber == 1)
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_4_1);
                else
                    attack = (GameObject)Instantiate(gameObject.GetComponent<AttackPrefabs>().fireMageSkill_4_2);
                break;
            default:
                break;
        }
        return attack;
    }
}
