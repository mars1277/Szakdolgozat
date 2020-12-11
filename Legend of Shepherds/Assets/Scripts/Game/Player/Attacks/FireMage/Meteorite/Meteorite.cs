using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

    public GameObject meteoriteDropGO;

    float speed = 2f;

        public Vector3 destination;
        public Vector3 startingPoint;
        public float delayTime;
        public int dmg;



    void Start()
    {
        delayTime = 3f;
        destination = GameObject.Find("ThirdSpellButton").transform.Find("DragArea").GetComponent<Active_Dragable_DragArea>().GetSkillUsedPosition();

        startingPoint = destination + new Vector3(delayTime * speed / 2f, delayTime * speed * 1.414f / 2f);

        GameObject drop = (GameObject)Instantiate(meteoriteDropGO);
        drop.transform.position = startingPoint;
        drop.GetComponent<MeteoriteDrop>().SetDelay(delayTime);

        Destroy(gameObject);
    }
}
