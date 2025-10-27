using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, RunAway }
    public EnemyState state = EnemyState.Idle;

    public int damage = 1;
    public float moveSpeed = 2f;
    public float traceRange = 15f;
    public float attackRange = 6f;
    public float RunRange = 10f;

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
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
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
                if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
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

    void RunAway()
    {
        if (CurrentHP >= maxHP / 5)
        {
            Vector3 dir = -(player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null) pc.PlayerTakeDamage(damage);

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
}

