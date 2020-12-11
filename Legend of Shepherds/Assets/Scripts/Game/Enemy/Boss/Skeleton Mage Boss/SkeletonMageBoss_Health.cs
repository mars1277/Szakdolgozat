using UnityEngine;
using System.Collections;

public class SkeletonMageBoss_Health : MonoBehaviour {


    float maximumHealth;
    public float currentHealth;
    float decayedDamage;

    public GameObject healthBar;
    public GameObject decayedDamageBar;

    int money;

    bool enemyIsKilled;

    void Start()
    {
        maximumHealth = 100000f;
        money = 1000;   
        currentHealth = maximumHealth;
        decayedDamage = currentHealth;

        enemyIsKilled = false;
    }

    void Update()
    {
        float health = currentHealth / maximumHealth;
        healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        float decay = decayedDamage / maximumHealth;
        decayedDamageBar.transform.localScale = new Vector3(decay, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);


    }

    public void SetLevel(int level)
    {
        switch (level)
        {
            case 1:
                maximumHealth = 100000f;
                money = 1000;
                break;
            case 2:
                maximumHealth = 700000f;
                money = 5000;
                break;
            case 3:
                maximumHealth = 3500000f;
                money = 20000;
                break;
        }
        currentHealth = maximumHealth;
        decayedDamage = currentHealth;
        gameObject.GetComponent<SkeletonMageBoss_Attack>().SetLevel(level);
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
            {
                GameObject.Find("Player").GetComponent<Player_Level>().AddXP(100000);
            }
            if(GameObject.Find("SkeletonMageBoss_Bomb(Clone)") != null)
                GameObject.Find("SkeletonMageBoss_Bomb(Clone)").GetComponent<SkeletonMageBoss_Bomb>().BossDied();

            DropGold();
            Destroy(gameObject);
        }
        StartCoroutine(DamageBarDecay(dmg));
    }

    public void DropGold()
    {
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position, money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.3f, 0f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.3f, 0f), money);

        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.15f, 0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.15f, -0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.15f, 0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.15f, -0.26f), money);

        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.6f, 0f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.6f, 0f), money);

        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.45f, 0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.45f, -0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.45f, 0.26f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.45f, -0.26f), money);

        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0f, 0.52f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0f, -0.52f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.3f, 0.52f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(-0.3f, -0.52f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.3f, 0.52f), money);
        GameObject.Find("Datas").GetComponent<AssumableController>().MakeGold(gameObject.transform.position + new Vector3(0.3f, -0.52f), money);

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
