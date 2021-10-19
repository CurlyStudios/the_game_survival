using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Enemy : MonoBehaviour
{
//stats
public int curHp;
public int maxHp;
public int scoreToGive;
//movement
public float moveSpeed;
public float attackRange;
public float yPathOffset;

private List<Vector3> path;

private Weapon weapon;
private GameObject target;
private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        target = FindObjectOfType<PlayerController>().gameObject;
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist <= attackRange)
        {
            if(weapon.CanShoot())
                weapon.Shoot();
        }
        else
        {
            ChaseTarget();
        }
        

    //rotate enemy
    Vector3 dir = (target.transform.position - transform.position).normalized;
    float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

    transform.eulerAngles = Vector3.up * angle;
    }

    public void TakeDamage (int damage)
    {
        curHp -= damage;

        if(curHp <= 0)
            Die();
    }
    void Die ()
    {
        Destroy(gameObject);
    }
    void ChaseTarget()
    {
        if(path.Count == 0)
            return;

            transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
    }
    void UpdatePath()
    {
        UnityEngine.AI.NavMeshPath navMeshPath = new UnityEngine.AI.NavMeshPath();
        UnityEngine.AI.NavMesh.CalculatePath(transform.position, target.transform.position, UnityEngine.AI.NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();
    }
}
