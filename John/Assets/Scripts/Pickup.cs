using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{


    public enum PickupType
    {
        Health,
        Ammo
    }
    public PickupType type;
    public int value;

    //BOBBING
    public float rotateSpeed;
    public float bobSpeed;
    public float bobHeight;

    private Vector3 startPos;
    private bool bobbingUp;


// Start is called before the first frame update
    void Start()
    {
        //set start pos
        startPos = transform.position;
    }

    void Update()
    {
        //rotate pickup
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        //up/down
        Vector3 offset = (bobbingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0));
        transform.position = Vector3.MoveTowards(transform.position, startPos + offset, bobSpeed * Time.deltaTime);

        if(transform.position == startPos + offset)
            bobbingUp = !bobbingUp;
    }
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Player player = other.GetComponent<player>();
            
            switch(type) 
            {
                case PickupType.Health:
                    player.GiveHealth(value);
                    break;
                case PickupType.Ammo:
                    player.GiveAmmo(value);
                    break;
            }
        }
    }



    public void GiveHealth ()
    {
        curHp = Mathf.Clamp(curHP + amountToGive, 0, maxHp);
    }

    public void GiveAmmo (int amountToGive)
    {
        weapon.curAmmo = Mathf.Clamp(weapon.curAmmo + amountToGive, 0, weapon.maxAmmo);
    }
    

    // Update is called once per frame
    
}
