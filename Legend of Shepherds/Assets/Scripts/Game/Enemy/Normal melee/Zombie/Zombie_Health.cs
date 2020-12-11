using UnityEngine;
using System.Collections;

public class Zombie_Health : MonoBehaviour {
    float maximumHealth;
    public float currentHealth;
    float decayedDamage;

    public GameObject healthBar;
    public GameObject decayedDamageBar;

    int level;
    int star;
    bool starBonusesSet;
    bool starNumberSet;

    int baseGold;
    int gold;
    int baseXP;
    int XP;
    int baseHealth;

    bool enemyIsKilled;

    void Start()
    {
        level = int.Parse(gameObject.name.Substring(12, 1));
        baseXP = 35;
        baseGold = 6;
        baseHealth = 680;

        XP = Mathf.RoundToInt(baseXP * Mathf.Pow(3, level - 1));

        gold = baseGold * (int)Mathf.Pow(5, level - 1);

        maximumHealth = baseHealth * (int)Mathf.Pow(10, level - 1);
        currentHealth = maximumHealth;
        decayedDamage = currentHealth;

        enemyIsKilled = false;
    }

    void Update()
    {
        if (starNumberSet && !starBonusesSet)
        {
            XP = Mathf.RoundToInt(XP * ((float)(star - 1) / 2f + 1));
            gold *= star;
            gold = Mathf.RoundToInt(gold * Random.Range(1f, 2f));
            maximumHealth *= (int)Mathf.Pow(2, star - 1);
            currentHealth = maximumHealth;
            decayedDamage = currentHealth;
            starBonusesSet = true;
        }
        float health = currentHealth / maximumHealth;
        healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        float decay = decayedDamage / maximumHealth;
        decayedDamageBar.transform.localScale = new Vector3(decay, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);
    }

    public void SetStar(int s)
    {
        star = s;
        gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<EnemyStarSetter>().SetStar(star);
        starNumberSet = true;
    }

    public void DealDamage(float dmg, string dmgDealer)
    {
        GameObject.Find("Player").GetComponent<Player_Health>().FairyPowerHealFromDamage((int)dmg);
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            if (!enemyIsKilled)
            {
                enemyIsKilled = true;
                GameObject.Find("Datas").GetComponent<DayManager>().enemyKilled++;
            }

            if (!dmgDealer.Equals("Wall"))
                GameObject.Find("Player").GetComponent<Player_Level>().AddXP(XP);

            GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position, gold);
            Destroy(gameObject);
        }
        StartCoroutine(DamageBarDecay(dmg));
    }

    public IEnumerator DamageBarDecay(float dmg)
    {
        int time = 50;
        int timeCounter = 0;
        float actualDmg = dmg / 50f;
        while (timeCounter < time)
        {
            yield return new WaitForSeconds(0.01f);
            timeCounter++;
            decayedDamage -= actualDmg;
        }
    }
}
