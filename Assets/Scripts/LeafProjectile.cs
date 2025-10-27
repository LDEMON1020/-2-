using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafProjectile : Projectile
{
    private Transform target;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Core")?.transform
        ?? GameObject.FindWithTag("Enemy")?.transform;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        TraceEnemy();
    }

    void TraceEnemy()
    {
        if (target == null)
        {
            Destroy(gameObject);
            if (player != null)
            {
                PlayerController controller = player.GetComponent<PlayerController>();
                if (controller != null)
                {
                    controller.currentMana += manaconsumption/3;
                }
            }
            return;
        }
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
        transform.LookAt(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Enemy2 enemy2 = other.GetComponent<Enemy2>();
        Core core = other.GetComponent<Core>();

        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            if (enemy != null)
                enemy.TakeDamage(damage);

            if (enemy2 != null)
                enemy2.TakeDamage(damage);
        }
        else if (other.CompareTag("Core"))
        {
            Destroy(gameObject);
            core.CoreTakeDamage(damage);
        }

    }
}
