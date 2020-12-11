using UnityEngine;
using System.Collections;

public class IceStorm : MonoBehaviour {

    float timer;
    bool collisionPerformed;
    bool ended;
    bool soundEnded;

    public ParticleSystem particleSystem1;
    float particles1;
    public ParticleSystem particleSystem2;
    float particles2;

    AudioSource audioSource;

    void Start () {
        timer = 0f;
        collisionPerformed = false;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector3(0f, 0f);
        ended = false;
        soundEnded = false;
        //particles1 = particleSystem1.emissionRate;
       // particles2 = particleSystem1.emissionRate;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update () {
        timer += Time.deltaTime;

        if (!collisionPerformed)
            if (timer > 0.3f)
            {
                gameObject.GetComponent<BoxCollider2D>().size = new Vector3(3f, 5f);
                collisionPerformed = true;
            }
        if(timer > 0.7f)
            gameObject.GetComponent<BoxCollider2D>().size = new Vector3(0f, 0f);

        if (!ended)
        {
            if (timer > 2.5f)
            {
                float tmp = (4.5f - timer) / 2f;
                //particleSystem1.emissionRate = particles1 * tmp;
                //particleSystem2.emissionRate = particles2 * tmp;
            }
            if (timer > 4.5f)
                ended = true;
        }

        if (!soundEnded)
        {
            if (timer > 3.5f)
            {
                float tmp = (5.5f - timer) / 2f;
                audioSource.volume = tmp;
            }
            if (timer > 5.5f)
                soundEnded = true;
        }

        if (timer > 6f)
            Destroy(gameObject);
	}
}
