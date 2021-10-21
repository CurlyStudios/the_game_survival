using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public int damage;
    public float lifetime;
    private float shootTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if alive time for bullet exceeds lifetime
        if(Time.time - shootTime >= lifetime)
            gameObject.SetActive(false);
    }
    void OnEnable() 
    {
        shootTime = Time.time;
    }
    void OnTriggerEnter(Collider other)
    {
        //did we hit the player?
        if(other.CompareTag("Player"))
            other.GetComponent<PlayerController>().TakeDamage(damage);
            else if(other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(damage);

            //Disable bullet
            gameObject.SetActive(false);
    }
}
