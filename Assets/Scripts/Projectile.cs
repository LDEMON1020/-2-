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
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            enemy.TakeDamage(damage);
        }
    }
}
