using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Army : MonoBehaviour
{
    public enum EnemyState { Idle, Trace }
    public EnemyState state = EnemyState.Idle;

    public int damage = 1;
    public float moveSpeed = 2f;
    public float traceRange = 15f;
    public float attackRange = 6f;

    public float lifeTime = 2f;

    private Transform Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        Enemy = GameObject.FindGameObjectWithTag("Enemy")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();

        if (Enemy == null) return;

        float dist = Vector3.Distance(Enemy.position, transform.position);

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < attackRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;
        }
    }

    void TracePlayer()
    {
        Vector3 dir = (Enemy.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(Enemy.position);
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        Enemy2 enemy2 = other.GetComponent<Enemy2>();

        if (other.CompareTag("Enemy"))
        {
            if (enemy != null)
                enemy.TakeDamage(damage);

            else if (enemy2 != null)
                enemy2.TakeDamage(damage);

            StartCoroutine(StopTemporarily(1f));
        }
    }
    IEnumerator StopTemporarily(float duration)
    {
        EnemyState prevState = state;
        float originalSpeed = moveSpeed;

        state = EnemyState.Idle;
        moveSpeed = 0f;

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        state = EnemyState.Trace;
    }
    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            Enemy = null;
            return;
        }

        GameObject closest = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(e.transform.position, currentPos);
            if (dist < minDist)
            {
                closest = e;
                minDist = dist;
            }
        }

        Enemy = closest?.transform;
    }
}

