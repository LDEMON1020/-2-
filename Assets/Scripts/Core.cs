using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    public GameObject EnemySpwaner;

    public int maxHP = 100;
    public int CurrentHP;

    public Slider hpSlider;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = maxHP;
        hpSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoreTakeDamage(int damage)
    {
        CurrentHP -= damage;
        hpSlider.value = (float)CurrentHP / maxHP;
        if (CurrentHP <= 0)
        {
            CoreDestroy();
        }
    }

    void CoreDestroy()
    {
        Destroy(gameObject);
        Destroy(EnemySpwaner);
    }
}
