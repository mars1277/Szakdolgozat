using UnityEngine;
using System.Collections;

public class Archer_Attack : MonoBehaviour {

    GameObject player;
    public GameObject arrowGO;

    float attackTimer;
    float attackSpeed;

    int star;

	void Start () {
        player = GameObject.Find("Player");
        attackSpeed = 2f;
        attackTimer = -5f;
        star = 1;
	}
	
	void Update () {
        attackTimer += Time.deltaTime;

        if(attackTimer > attackSpeed)
        {
            //shoot
            Vector3 diff = player.transform.position - gameObject.transform.position;
            float rotation = 0f;
            //rotation
            if (diff.x == 0f)
            {
                if (diff.y <= 0f)
                    rotation = 0f;
                if (diff.y > 0f)
                    rotation = 180f;
            }
            if(diff.x < 0f)
            {
                if (diff.y < 0f)
                    rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
                if (diff.y == 0f)
                {
                    rotation = 0f;
                }
                if (diff.y > 0f)
                    rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
            }

            if (diff.x > 0f)
            {
                if (diff.y < 0f)
                    rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI;
                if (diff.y == 0f)
                {
                    rotation = 0f;
                }
                if (diff.y > 0f)
                    rotation = -Mathf.Atan(diff.x / diff.y) * 180 / Mathf.PI + 180f;
            }

            Quaternion rot = Quaternion.Euler(0f, 0f, rotation);
            Vector3 vec = new Vector3(Mathf.Sin(-rotation * Mathf.PI / 180f), Mathf.Cos(-rotation * Mathf.PI / 180f));

            vec = (vec.magnitude > 1) ? vec.normalized : vec;

            GameObject arrow = (GameObject)Instantiate(arrowGO);
            arrow.GetComponent<Archer_Arrow>().SetWay(vec);
            arrow.GetComponent<Archer_Arrow>().SetStar(star);
            arrow.transform.position = gameObject.transform.position + new Vector3(-vec.x * 0.13f,- 0.09f - vec.y * 0.11f);
            arrow.transform.rotation = rot;

            attackTimer = 0f;
        }
	
	}

    public void SetStar(int s)
    {
        star = s;
    }
}
