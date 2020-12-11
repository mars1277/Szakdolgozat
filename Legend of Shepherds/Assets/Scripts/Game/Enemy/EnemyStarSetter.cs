using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStarSetter : MonoBehaviour
{

    public void SetStar(int starCount)
    {
        for (int i = 0; i < 3; i++)
        {
            if (starCount - 1 != i)
            {
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}