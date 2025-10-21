using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab1;
    public GameObject projectilePrefab2;
    public Transform firePoint;
    Camera cam;
    public bool Fastmode = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwap();

        if (Fastmode == false && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Fastmode == true && Input.GetMouseButtonDown(0))
        {
            Shoot2();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab1, firePoint.position, Quaternion.LookRotation(direction));
    }
    void Shoot2()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        GameObject proj = Instantiate(projectilePrefab2, firePoint.position, Quaternion.LookRotation(direction));
    }
    void WeaponSwap()
    {
        if (Fastmode == false && Input.GetKeyDown(KeyCode.Z))
        {
            Fastmode = true;
        }
        else if (Fastmode == true && Input.GetKeyDown(KeyCode.Z))
        {
            Fastmode = false;
            
        }
    }
}
