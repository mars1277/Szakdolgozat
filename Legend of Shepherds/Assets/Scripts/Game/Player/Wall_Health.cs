using UnityEngine;
using System.Collections;

public class Wall_Health : MonoBehaviour {

    public GameObject dayFailedCanvas;

    bool failed;
    float failedCounter;

    float maximumHealth;
    float currentHealth;
    float decayedDamage;

    public GameObject healthBar;
    public GameObject decayedDamageBar;

    void Start()
    {
        maximumHealth = GameObject.Find("Datas").GetComponent<Datas>().Get_DurabityPoints_Wall(PlayerPrefs.GetInt("Slot" + PlayerPrefs.GetInt("GameSlot") + "_Level"));
        currentHealth = maximumHealth;
        decayedDamage = currentHealth;

        SetMaxHealthDigits();
        SetCurrentHealthDigits();
        failed = false;
    }

    void Update()
    {
        if (!failed)
        {
            SetCurrentHealthDigits();

            float health = currentHealth / maximumHealth;
            healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

            float decay = decayedDamage / maximumHealth;
            decayedDamageBar.transform.localScale = new Vector3(decay, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);


        }
        else
        {
            if (failedCounter < 0.5f)
            {
                failedCounter += Time.deltaTime;
                Time.timeScale = 1f - failedCounter * 2;
            }
            else
            {
                failedCounter = 0.5f;
                Time.timeScale = 0f;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Enemy")
        {
            float damage = collider.gameObject.GetComponent<DamageDealer>().HealthValue();
            collider.gameObject.GetComponent<DamageDealer>().DealDamage(damage, "Wall");
            DealDamage(damage);
            if(currentHealth <= 0)
            {
                if (!failed)
                {
                    currentHealth = 0f;
                    decayedDamage = 0f;
                    SetCurrentHealthDigits();
                    float health = currentHealth / maximumHealth;
                    healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
                    float decay = decayedDamage / maximumHealth;
                    decayedDamageBar.transform.localScale = new Vector3(decay, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);
                    GameObject dfc = (GameObject)Instantiate(dayFailedCanvas);
                    GameObject.Find("Player").GetComponent<Player_Level>().SaveChanges_PlayerLost();
                    failed = true;
                    failedCounter = 0f;
                }
            }
        }
    }

    public float GetHealhtPercentage()
    {
        float percentage = currentHealth / maximumHealth;
        return percentage;
    }

    public void DealDamage(float dmg)
    {
        currentHealth -= dmg;
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

    void SetMaxHealthDigits()
    {

        int numberOfMaxHealthDigits = 0;

        if (maximumHealth >= 1000000)
            numberOfMaxHealthDigits = 7;
        else
        if (maximumHealth >= 100000)
            numberOfMaxHealthDigits = 6;
        else
        if (maximumHealth >= 10000)
            numberOfMaxHealthDigits = 5;
        else
        if (maximumHealth >= 1000)
            numberOfMaxHealthDigits = 4;
        else
        if (maximumHealth >= 100)
            numberOfMaxHealthDigits = 3;
        else
        if (maximumHealth >= 10)
            numberOfMaxHealthDigits = 2;
        else
        if (maximumHealth >= 1)
            numberOfMaxHealthDigits = 1;


        GameObject dm1000000 = GameObject.Find("MillionMaximumDurabity");
        GameObject dm100000 = GameObject.Find("HundredThousandMaximumDurabity");
        GameObject dm10000 = GameObject.Find("TenThousandMaximumDurabity");
        GameObject dm1000 = GameObject.Find("ThousandMaximumDurabity");
        GameObject dm100 = GameObject.Find("HundredMaximumDurabity");
        GameObject dm10 = GameObject.Find("TenMaximumDurabity");
        GameObject dm1 = GameObject.Find("OneMaximumDurabity");
        

        switch (numberOfMaxHealthDigits)
        {
            case 1:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 2:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 3:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10 % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 4:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 1000);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100 % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10 % 10);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 5:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10000);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 1000 % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100 % 10);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10 % 10);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 6:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100000);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10000 % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 1000 % 10);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100 % 10);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10 % 10);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                break;
            case 7:
                dm1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 1000000);
                dm100000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100000 % 10);
                dm10000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10000 % 10);
                dm1000.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 1000 % 10);
                dm100.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 100 % 10);
                dm10.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth / 10 % 10);
                dm1.GetComponent<ChangeDigitSprite>().ChangeSprite(maximumHealth % 10);
                break;
            default:
                break;
        }

    }

    void SetCurrentHealthDigits()
    {
        int numberOfCurrentHealthDigits = 0;

        if (currentHealth >= 1000000)
            numberOfCurrentHealthDigits = 7;
        else
        if (currentHealth >= 100000)
            numberOfCurrentHealthDigits = 6;
        else
        if (currentHealth >= 10000)
            numberOfCurrentHealthDigits = 5;
        else
        if (currentHealth >= 1000)
            numberOfCurrentHealthDigits = 4;
        else
        if (currentHealth >= 100)
            numberOfCurrentHealthDigits = 3;
        else
        if (currentHealth >= 10)
            numberOfCurrentHealthDigits = 2;
        else
        if (currentHealth >= 0)
            numberOfCurrentHealthDigits = 1;


        GameObject dc1000000 = GameObject.Find("MillionCurrentDurabity");
        GameObject dc100000 = GameObject.Find("HundredThousandCurrentDurabity");
        GameObject dc10000 = GameObject.Find("TenThousandCurrentDurabity");
        GameObject dc1000 = GameObject.Find("ThousandCurrentDurabity");
        GameObject dc100 = GameObject.Find("HundredCurrentDurabity");
        GameObject dc10 = GameObject.Find("TenCurrentDurabity");
        GameObject dc1 = GameObject.Find("OneCurrentDurabity");
        

        switch (numberOfCurrentHealthDigits)
        {
            case 1:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth);
                break;
            case 2:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            case 3:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10 % 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            case 4:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 1000);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100 % 10);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10 % 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            case 5:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10000);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 1000 % 10);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100 % 10);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10 % 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            case 6:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(-1);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100000);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10000 % 10);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 1000 % 10);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100 % 10);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10 % 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            case 7:
                dc1000000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 1000000);
                dc100000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100000 % 10);
                dc10000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10000 % 10);
                dc1000.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 1000 % 10);
                dc100.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 100 % 10);
                dc10.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth / 10 % 10);
                dc1.GetComponent<ChangeDigitSprite>().ChangeSprite(currentHealth % 10);
                break;
            default:
                break;
        }

    }
}