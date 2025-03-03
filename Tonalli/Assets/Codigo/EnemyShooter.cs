using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject fireballPrefab; // Proyectil
    public Transform firePoint; // Lugar desde donde dispara
    public float fireRate = 2f; // Cada cuánto dispara

    private Animator anim;
    private float nextFireTime;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack"); // Activa animación
        Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
    }
}
