using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageDealer : MonoBehaviour {

    public GameObject damageValueCanvas;

    public void DealDamage(float dmg, string dmgDealer = "Something")
    {
        if(gameObject.name.Substring(0, 7) == "Goblin_")
        {
            gameObject.GetComponent<Goblin_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
        if (gameObject.name.Substring(0, 7) == "Archer_")
        {
            gameObject.GetComponent<Archer_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
        if (gameObject.name.Substring(0, 7) == "Zombie_")
        {
            gameObject.GetComponent<Zombie_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
        if (gameObject.name.Substring(0, 4) == "Bat_")
        {
            gameObject.GetComponent<Bat_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
        if (gameObject.name.Substring(0, 13) == "SkeletonMage_")
        {
            gameObject.GetComponent<SkeletonMage_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
        if (gameObject.name.Substring(0, 16) == "SkeletonMageBoss")
        {
            gameObject.GetComponent<SkeletonMageBoss_Health>().DealDamage(dmg, dmgDealer);
            MakeDealtDamageText(Mathf.RoundToInt(dmg));
        }
    }

    public void MakeDealtDamageText(int dmg)
    {
        Vector3 pos = gameObject.transform.position;
        pos = new Vector3((pos.x + 1.44f) * Screen.width / 2.88f, (pos.y + 2.5f) * Screen.height / 5.12f);
        GameObject damageValue = (GameObject)Instantiate(damageValueCanvas);
        Transform text = damageValue.transform.Find("DamageValueText");
        text.gameObject.GetComponent<Text>().text = dmg.ToString();
        text.transform.position = pos + new Vector3(0f, Screen.height / 25f);
    }

    public void DealDamageOverTime(float dmg, float overallTime, float tickTime, string dmgDealer = "Something")
    {
        StartCoroutine(DOT(dmg, overallTime, tickTime, dmgDealer));
    }

    public IEnumerator DOT(float dmg, float overallTime, float tickTime, string dmgDealer)
    {
        float tickCounter = 0;
        float ticks = overallTime / tickTime;

        float actualDmg = dmg / ticks;
        while (tickCounter < ticks)
        {
            DealDamage(Mathf.RoundToInt(actualDmg * Random.Range(0.8f, 1.2f)), dmgDealer);
            yield return new WaitForSeconds(tickTime);
            tickCounter++;
        }
    }

    public int HealthValue()
    {
        if (gameObject.name.Substring(0, 7) == "Goblin_")
        {
            return (int)gameObject.GetComponent<Goblin_Health>().currentHealth;
        }
        if (gameObject.name.Substring(0, 7) == "Archer_")
        {
            return (int)gameObject.GetComponent<Archer_Health>().currentHealth;
        }
        if (gameObject.name.Substring(0, 7) == "Zombie_")
        {
            return (int)gameObject.GetComponent<Zombie_Health>().currentHealth;
        }
        if (gameObject.name.Substring(0, 4) == "Bat_")
        {
            return (int)gameObject.GetComponent<Bat_Health>().currentHealth;
        }
        if (gameObject.name.Substring(0, 13) == "SkeletonMage_")
        {
            return (int)gameObject.GetComponent<SkeletonMage_Health>().currentHealth;
        }
        if (gameObject.name.Substring(0, 16) == "SkeletonMageBoss")
        {
            return (int)gameObject.GetComponent<SkeletonMageBoss_Health>().currentHealth;
        }
        return 0;
    }  
}
