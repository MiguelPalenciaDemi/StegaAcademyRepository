using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    float health = 10;
    
    [Header("Combat")]
    [SerializeField]
    Transform target;
    [SerializeField]
    float distanceAttack = 5f;
    [SerializeField]
    float turnRate = 50f;
    [SerializeField]
    Collider weaponCollider;

    PlayerStates state;

    [Header("Sight")]
    [SerializeField]
    Transform originRaycastPoint;
    Ray ray;
    RaycastHit hit;


    NavMeshAgent navMeshAgent;
    Animator animator;
    EnemySpawner ownerSpawner;
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<PlayerStates>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state.PlayerState != PlayerState.dead) // To avoid enemies move while they are dyin
        {
            Animate();
            PlayerAwareRaycast();
            if (CanMove())
                Move();
            else //state = attacking or hurt
                RotateToTarget();
            
        
        }

    }

    //Attack or not
    void PlayerAwareRaycast() 
    {
        ray.origin = originRaycastPoint.position;
        ray.direction = target.position - originRaycastPoint.position;
        
        if(Physics.Raycast(ray,out hit, distanceAttack)) 
        {            
            if(hit.transform.tag == "Player" && CanAttack()) 
            {
                navMeshAgent.isStopped = true;
                state.SetAttacking();
                animator.SetBool("isAttacking", true);
                
            }
        }
    }

    bool CanAttack() 
    {

        return state.PlayerState != PlayerState.attacking && state.PlayerState != PlayerState.hurt;//We check if we are not attacking beacuse otherwise it will spawn a lot of projectiles
    }

    bool CanMove() 
    {
        return state.PlayerState != PlayerState.attacking &&  state.PlayerState != PlayerState.hurt;//Hurt stop it


    }

    void Move() 
    {
        navMeshAgent.isStopped = false;        
        navMeshAgent.SetDestination(target.position);
    }

    void Animate() 
    {
        animator.SetBool("isWalking", !navMeshAgent.isStopped);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "HurtWeapon")
        {
            Destroy(collision.gameObject);
            GetHurt(5);//Provisional, hacer daño de armas

        }
    }

    private void GetHurt(float damage)
    {
        animator.SetTrigger("getHurt");
        state.PlayerState = PlayerState.hurt;

        health -= damage;
        if(health <= 0)
        {
            state.PlayerState = PlayerState.dead;
            animator.SetTrigger("death");
            navMeshAgent.isStopped = true;

        }
    }

    void RotateToTarget()
    {
        //Find the rotation desired
        Vector3 targetForward = target.position - transform.position;
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, targetForward, turnRate * Time.deltaTime, 0);

        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        //Apply rotation
        transform.rotation = rotation;
    }

    void Die() 
    {
        ownerSpawner.RemoveEnemy(this);
        Destroy(gameObject);

    }

    void BackFromHurt()
    {

        if(state.PlayerState != PlayerState.dead) //There isn't return from death
        {
            state.SetIdle();
        }
    }

    public void SetTarget(Transform newTarget) 
    {

        target = newTarget;
    }
    public void SetSpawner(EnemySpawner newSpawner) 
    {
        ownerSpawner = newSpawner;
    }
}
