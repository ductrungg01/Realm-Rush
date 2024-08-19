using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    private Transform target;

    [SerializeField][Min(0)] private float attackRange = 15f;

    private ParticleSystem projectileParticles;

    private void Awake()
    {
        projectileParticles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;

        float minDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < minDistance)
            {
                minDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        if (target == null) return;

        float distance = Vector3.Distance (transform.position, target.transform.position);

        Attack(distance <= attackRange);

        weapon.LookAt(target);
    }

    private void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }
}
