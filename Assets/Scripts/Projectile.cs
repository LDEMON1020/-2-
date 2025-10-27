using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public float speed = 20f;
    public float lifeTime = 2f;

    public int manaconsumption = 3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
