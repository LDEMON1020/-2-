using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] projectilePrefabs;

    public Spinning Spinning;

    private int currentWeaponIndex = 0;

    public bool attackMode = false;

    public Transform firePoint;
    Camera cam;
  
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        SelectWeapon(currentWeaponIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == true && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        WeaponSwap();
    }

    void Shoot()
    {
            if (currentWeaponIndex < 0 || currentWeaponIndex >= projectilePrefabs.Length)
            {
                return;
            }
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint;
            targetPoint = ray.GetPoint(50f);
            Vector3 direction = (targetPoint - firePoint.position).normalized;

            GameObject projectilePrefab = projectilePrefabs[currentWeaponIndex];
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
    }
   
    void WeaponSwap()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            currentWeaponIndex--;
            if (currentWeaponIndex < 0)
                currentWeaponIndex = weapons.Length - 1;
            SelectWeapon(currentWeaponIndex);
        }
        else if (scroll < 0f)
        {
            currentWeaponIndex++;
            if (currentWeaponIndex >= weapons.Length)
                currentWeaponIndex = 0;
            SelectWeapon(currentWeaponIndex);
        }
    }

    void SelectWeapon(int index)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == index);
        }
    }
}
