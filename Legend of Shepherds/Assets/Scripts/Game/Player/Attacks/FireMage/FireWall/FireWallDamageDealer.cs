using UnityEngine;
using System.Collections;

public class FireWallDamageDealer : MonoBehaviour {

    float dmg;

    void Start()
    {
        dmg = GameObject.Find("Datas").GetComponent<Datas>().Get_FireWallDamage_Player(1) * GameObject.Find("Datas").GetComponent<Datas>().Get_AdvancementValues(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        dmg += GameObject.Find("Datas").GetComponent<Datas>().Get_FireWallDamage_Player(0) * GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_MagicPower();
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMagicDamage() / 100f);
        dmg *= (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusAllDmgDone() / 100f);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<DamageDealer>().DealDamageOverTime(dmg, 10f, 2f);
        }
    }
}
