using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, RunAway }
    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;
    public float traceRange = 15f;
    public float attackRange = 6f;
    public float RunRange = 10f;

    public float attackCooldown = 1.5f;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform player;
    private float lastAttackTime;

    public int maxHP = 5;
    public int CurrentHP;

    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = maxHP;
        hpSlider.value = 1f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = -attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < attackRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                    break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else if (CurrentHP <= 1)
                    state = EnemyState.RunAway;
                else
                    AttackPlayer();
                break;
            case EnemyState.RunAway:
                if (dist > RunRange)
                    state = EnemyState.Idle;
                else if (CurrentHP <= 1)
                    RunAway();
                break;
               
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        hpSlider.value = (float)CurrentHP / maxHP;
        if (CurrentHP <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if(ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

   void RunAway()
    {
        if(CurrentHP >= maxHP/5)
        {
            Vector3 dir = -(player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
