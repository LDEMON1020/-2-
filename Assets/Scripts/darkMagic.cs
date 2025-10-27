using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkMagic : Projectile
{
   public GameObject[] Undead;
    public float SpawnRange = 5f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 spawnPos = new Vector3(
                 transform.position.x + Random.Range(-SpawnRange, SpawnRange),
                 transform.position.y,
                 transform.position.z + Random.Range(-SpawnRange, SpawnRange)
                 );
            int randomIndex = Random.Range(0, Undead.Length);
            Instantiate(Undead[randomIndex], spawnPos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnRange * 2, 0.1f, SpawnRange * 2));
    }
}
