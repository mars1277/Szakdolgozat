using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Health : MonoBehaviour
{
    public GameObject dayFailedCanvas;

    public bool failed;
    float failedCounter;

    float maximumHealth;
    float currentHealth;
    float decayedDamage;

    public GameObject healthBar;
    public GameObject decayedDamageBar;

    int armor;

    float healthRegencounter;
    float healthRegenAmount;

    float fairyPowerDamageReducation;
    bool fairyPowerHealFromDamageActivated;

    int gold;

    public Text goldText;
    public Image goldImage;
    public Text silverText;
    public Image silverImage;
    public Text bronzeText;

    public void Initialize()
    {
        maximumHealth = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Health();
        currentHealth = maximumHealth;
        decayedDamage = currentHealth;

        SetMaxHealthDigits();

        armor = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Armor();

        healthRegencounter = 0;
        healthRegenAmount = GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_HealthRegen();

        fairyPowerHealFromDamageActivated = false;

        failed = false;
        gold = 0;
        SetMoneyValues(gold);
    }

    void Update()
    {
        if (!failed)
        {
            healthRegencounter += Time.deltaTime;
            if (healthRegencounter > 2f)
            {
                if (currentHealth < maximumHealth)
                    currentHealth += healthRegenAmount;
                healthRegencounter = 0;
            }
            if (currentHealth > maximumHealth)
            {
                currentHealth = maximumHealth;
            }

            float health = currentHealth / maximumHealth;
            healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

            float decay = decayedDamage / maximumHealth;
            decayedDamageBar.transform.localScale = new Vector3(0f, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);

            SetCurrentHealthDigits();
        }
        else
        {
            GameObject.Find("VirtualJoystickContainer").GetComponent<VirtualJoystick>().Stop();
            if (failedCounter <= 1f)
            {
                failedCounter += Time.deltaTime;
                Time.timeScale = 1f - failedCounter;
            }
            else
            {
                failedCounter = 1f;
                Time.timeScale = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!failed)
        {
            if (collider.tag == "Enemy")
            {
                float damage;
                damage = collider.GetComponent<DamageDealer>().HealthValue();

                DealDamage(damage);
                if (currentHealth <= 0f)
                {
                    Death();
                }
                collider.GetComponent<DamageDealer>().DealDamage(damage);
            }
            else
            if(collider.tag == "Assumable")
            {
                gold += collider.GetComponent<Gold>().GetGold();
                SetMoneyValues(gold);
                collider.GetComponent<Gold>().DestroyObject();
            }
        }
    }

    public void DealDamage(float dmg)
    {
        if (!failed)
        {
            dmg -= GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_Armor();
            if (dmg < 0)
                dmg = 0;
            dmg *= (1f - fairyPowerDamageReducation);
            currentHealth -= dmg;
            if (currentHealth <= 0f)
                Death();
        }
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

    public void Kill()
    {
        if (currentHealth <= 0f)
            Death();
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetMoneyValues(int gold)
    {
        if (gold >= 10000)
        {
            goldText.text = (gold / 10000).ToString();
            silverText.text = (gold % 10000 / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
            goldImage.color = new Color(1f, 1f, 1f, 1f);
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (gold >= 100)
        {
            goldText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverText.text = (gold / 100).ToString();
            bronzeText.text = (gold % 100).ToString();
            silverImage.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            goldText.text = "";
            silverText.text = "";
            goldImage.color = new Color(1f, 1f, 1f, 0f);
            silverImage.color = new Color(1f, 1f, 1f, 0f);
            bronzeText.text = gold.ToString();
        }
    }

    public void Death()
    {
        if (!failed)
        {
            currentHealth = 0f;
            decayedDamage = 0f;
            float health = currentHealth / maximumHealth;
            healthBar.transform.localScale = new Vector3(health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            float decay = decayedDamage / maximumHealth;
            decayedDamageBar.transform.localScale = new Vector3(decay, decayedDamageBar.transform.localScale.y, decayedDamageBar.transform.localScale.z);
            failed = true;
            GameObject dfc = Instantiate(dayFailedCanvas);
            SetCurrentHealthDigits();
            GameObject.Find("Player").GetComponent<Player_Level>().SaveChanges_PlayerLost();
            GameObject.Find("Player").GetComponent<Player_Movement>().failed = true;
            failedCounter = 0f;
        }
    }

    public void SetLevelUpHealth(float newHealth)
    {
        maximumHealth = newHealth;
        currentHealth = newHealth;
        StopAllCoroutines();
        decayedDamage = newHealth;
        SetMaxHealthDigits();
    }

    public void SwitchOnDamageReducation(float value)
    {
        fairyPowerDamageReducation = value / 100f;
    }

    public void SwitchOffDamageReducation()
    {
        fairyPowerDamageReducation = 0f;
    }

    public void FairyPowerBaseHealthHeal()
    {
        float healingAmount = 0.2f * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f);
        int baseHealth = (int)GameObject.Find("Datas").GetComponent<Datas>().Get_HealthPoints_Player(GameObject.Find("Player").GetComponent<Player_Level>().GetLevel());
        float tmp1 = (1f - (float)currentHealth / (float)maximumHealth);
        tmp1 = (healingAmount + tmp1 * healingAmount) * maximumHealth;
        tmp1 = Mathf.RoundToInt(tmp1);
        currentHealth += tmp1;
        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
    }

    public void FairyPowerMaxHealthHealOverTime()
    {
        float healingAmount = (20f * (1f + GameObject.Find("Datas").GetComponent<AttributeCalculator>().Get_BonusMedallionPower() / 100f)) / 100f;
        float tmp1 = maximumHealth * healingAmount;
        StartCoroutine(HealingOverTime(tmp1, 10));
    }

    public void FairyPowerHealFromDamage(int damage)
    {
        if (fairyPowerHealFromDamageActivated)
        {
            int healingAmount = Mathf.CeilToInt(damage * 0.05f);
            currentHealth += healingAmount;
            if (currentHealth > maximumHealth)
                currentHealth = maximumHealth;
        }
    }

    public void ActivateFairyPowerHealFromDamage()
    {
        fairyPowerHealFromDamageActivated = true;
    }

    public void DeactivateFairyPowerHealFromDamage()
    {
        fairyPowerHealFromDamageActivated = false;
    }

    public IEnumerator HealingOverTime(float healing, int time)
    {
        int timeCounter = 0;
        float actualHealing = healing / (float)time;
        while (timeCounter < time)
        {
            yield return new WaitForSeconds(1f);
            timeCounter++;
            currentHealth += actualHealing;
            if (currentHealth > maximumHealth)
                currentHealth = maximumHealth;
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


        GameObject dm1000000 = GameObject.Find("MillionMaximumHealth");
        GameObject dm100000 = GameObject.Find("HundredThousandMaximumHealth");
        GameObject dm10000 = GameObject.Find("TenThousandMaximumHealth");
        GameObject dm1000 = GameObject.Find("ThousandMaximumHealth");
        GameObject dm100 = GameObject.Find("HundredMaximumHealth");
        GameObject dm10 = GameObject.Find("TenMaximumHealth");
        GameObject dm1 = GameObject.Find("OneMaximumHealth");


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



        GameObject dc1000000 = GameObject.Find("MillionCurrentHealth");
        GameObject dc100000 = GameObject.Find("HundredThousandCurrentHealth");
        GameObject dc10000 = GameObject.Find("TenThousandCurrentHealth");
        GameObject dc1000 = GameObject.Find("ThousandCurrentHealth");
        GameObject dc100 = GameObject.Find("HundredCurrentHealth");
        GameObject dc10 = GameObject.Find("TenCurrentHealth");
        GameObject dc1 = GameObject.Find("OneCurrentHealth");


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