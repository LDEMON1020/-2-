using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 2;
    public float speed = 8f;
    public float lifeTime = 3f;

    private Vector3 movDir;

    public void SetDirection(Vector3 dir)
    {
        movDir = dir.normalized;
    }
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movDir * speed * Time.deltaTime;
    }

void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.GetComponent<PlayerController>();
            if (pc != null) pc.PlayerTakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
