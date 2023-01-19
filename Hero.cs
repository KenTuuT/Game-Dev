using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public int health = 100;
    public Color hitColor = Color.white;
    public float moveSpeed = 10.0f;
    public float rotateSpeed = 100.0f;
    public int magicOrbAmount = 20;
    public GameObject magicOrb = null;
    public Transform socket = null;

    private Color originalColor = Color.red;

    void Awake()
    {
        originalColor = this.GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed;
        float rotation = Input.GetAxis("Horizontal") * rotateSpeed;
        this.transform.Translate(0, 0, move * Time.deltaTime);
        this.transform.Rotate(0, rotation * Time.deltaTime, 0);
    }

    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(magicOrbAmount > 0)
            {
                magicOrbAmount--;
                GameObject obj = Instantiate(magicOrb,socket.position, socket.rotation) as GameObject;
                obj.name = "magicOrb";
            }                
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "cannonball")
        {
            int hp = other.GetComponent<CannonBall>().hitpoint;
            GetHealth(hp);
        }
    }

    void GetHealth(int hp)
    {
        if (health > 0)
        {
            health -= hp;
            StartCoroutine(GetHit());
            Debug.Log("Hero health: " + health);
        }
        else
        {
            Debug.Log("GameOver");
        }
    }

    IEnumerator GetHit()
    {
        this.GetComponent<Renderer>().material.color = hitColor;
        yield return new WaitForSeconds(0.4f);
        this.GetComponent<Renderer>().material.color = originalColor;
    }   
}
