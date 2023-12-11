using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField]
    Transform shootPoint;
    [SerializeField]
    GameObject prefabBullet;
    [SerializeField]
    Collider weaponCollider;
    [SerializeField]
    float attackRate = 4;
    
    Animator animator;
    PlayerStates state;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = GetComponent<PlayerStates>();
    }
  


    void RangeAttack()
    {
        Projectile bullet = Instantiate(prefabBullet, shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();
        bullet.Shoot();
        state.Invoke("SetIdle", attackRate);
        animator.SetBool("isAttacking", false);

    }

    void DesactivateMeleeAttack()
    {
        weaponCollider.enabled = false;
        animator.SetBool("isAttacking", false);
        state.Invoke("SetIdle", attackRate);
    }
    void ActivateMeleeAttack()
    {
        weaponCollider.enabled = true;

    }

    
}
