using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool;
    public Transform muzzle;
    public PlayerController player;

    public int curAmmo;
    public int maxAmmol;
    public bool infiniteAmmo;

    public float bulletSpeed;

    public float fireRate;
    private float lastShootTime;
    private bool isPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerController>();
        //is the weapon attached to the player?
        if(player)
            isPlayer = true;
    }

    //can we shoot? - do we have ammo?
    public bool CanShoot ()
    {
        if(Time.time - lastShootTime >= fireRate)
        {
            if(curAmmo > 0 || infiniteAmmo == true)
                return true;
        }
        return false;
    }
    public void Shoot()
    {
        lastShootTime = Time.time;
        curAmmo--;

         GameObject bullet = bulletPool.GetObject();

         bullet.transform.position = muzzle.position;
         bullet.transform.rotation = muzzle.rotation;

        //Set Velocity
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
