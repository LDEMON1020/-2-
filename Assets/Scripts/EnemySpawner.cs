using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnInterval = 3f;
    public float SpawnRange = 5f;

    private float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            Vector3 spawnPos = new Vector3(
                transform.position.x + Random.Range(-SpawnRange, SpawnRange),
                transform.position.y,
                transform.position.z + Random.Range(-SpawnRange, SpawnRange)
                );

            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomIndex], spawnPos, Quaternion.identity);
            timer = 0f;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnRange * 2, 0.1f, SpawnRange * 2));
    }
}
