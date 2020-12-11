using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireWall : MonoBehaviour
{

    public GameObject fire_sprite_1;
    public GameObject fire_sprite_2;
    public GameObject fire_sprite_3;
    public GameObject damageCollider;

    float counter = 0f;
    float lifetimeCounter = 8f;

    float dmg;
    int targetHealth;
    public GameObject damageValueCanvas;

    void Update()
    {
        counter += Time.deltaTime;
        lifetimeCounter -= Time.deltaTime;

        if (lifetimeCounter < 0f)
        {
            Destroy(gameObject);
        }

        if (counter > 3f)
        {
            counter = 0f;
        }
        else
        {
            if (counter < 1f)
            {
                fire_sprite_1.transform.localScale = new Vector3(0.5f, 0.4f);
                fire_sprite_2.transform.localScale = new Vector3(0.5f, 0.4f + counter * 0.1f);
                fire_sprite_3.transform.localScale = new Vector3(0.5f, 0.4f - counter * 0.1f);
            }
            else if (counter < 2f)
            {
                fire_sprite_1.transform.localScale = new Vector3(0.5f, 0.4f + (counter - 1f) * 0.1f);
                fire_sprite_2.transform.localScale = new Vector3(0.5f, 0.5f - (counter - 1f) * 0.1f);
                fire_sprite_3.transform.localScale = new Vector3(0.5f, 0.3f);
            }
            else if (counter < 3f)
            {
                fire_sprite_1.transform.localScale = new Vector3(0.5f, 0.5f - (counter - 2f) * 0.1f);
                fire_sprite_2.transform.localScale = new Vector3(0.5f, 0.4f);
                fire_sprite_3.transform.localScale = new Vector3(0.5f, 0.3f + (counter - 2f) * 0.1f);
            }
        }
    }


}
