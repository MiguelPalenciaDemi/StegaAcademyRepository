using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float force;
    Rigidbody rb;

    int lifeSpan = 5;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        Destroy(gameObject,lifeSpan);

    }

    public void Shoot()
    {        
        rb.AddForce(CalculateForce(), ForceMode.Impulse);                
    }

    public float GetInitialSpeed() 
    {       
        return force;    
    }

    Vector3 CalculateForce() 
    {
        Vector3 forceDirection = -transform.up * force;
        
        return forceDirection;
    }

    private void OnTriggerEnter(Collider other)    
    {
        if(other.transform.tag == "Wall")
            Destroy(gameObject);
    }
}
