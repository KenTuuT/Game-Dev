using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int hitpoint = 20;
    public float minForce = 400.0f;
    public float maxForce = 700.0f;
    public float delayTime = 2.0f;
    public AudioClip audioHit = null;
    public AudioClip audioShoot = null;
    public ParticleSystem particle =null;
    private bool isActive = true;

    void Awake()
    {
        this.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, GetRandomValue(), GetRandomValue()));
        this.GetComponent<AudioSource>().PlayOneShot(audioShoot);
    }

    float GetRandomValue()
    {
        return Random.Range(minForce, maxForce);
    }

    void OnTriggerEnter(Collider other)
    {
        if(isActive)
        {
            isActive = false;
            
            this.GetComponent<AudioSource>().PlayOneShot(audioHit);

            StartCoroutine(DisableParticle());
        }
    }

    IEnumerator DisableParticle()
    {
        yield return new WaitForSeconds(delayTime);
        particle.enableEmission = false;
        hitpoint = 0;
    }
}
