using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;
        

    [SerializeField]
    Transform shootPoint;
    Animator anim; 
    PlayerStates state;

    TrajectoryPredictor trajectoryPredictor;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        state = GetComponent<PlayerStates>();
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetState() == GameState.play) 
        {
            if(Input.GetMouseButtonDown(0) && state.PlayerState == PlayerState.idle)//Button Down = Aim
            {
                anim.SetBool("isAttacking", true);
                state.SetAttacking();
            }
            else if(Input.GetMouseButton(0) && state.PlayerState == PlayerState.attacking)//keep down = aim + rotate towards mouse position
            {
                RotateAttack();
            }
        
            else if (!Input.GetMouseButton(0) && state.PlayerState == PlayerState.attacking)// Release = shoot
            {            
                anim.SetBool("isAttacking", false);
                state.SetIdle();
            }        
        
        }
    }

    public void LaunchProjectile() 
    {
        Projectile bullet = Instantiate(prefab, shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();
        
        bullet.Shoot();
    }

    void RotateAttack()
    {
        //Get Mouse position on world
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mousePosition;
        if(Physics.Raycast(ray, out hit,1000))
        {
            mousePosition = hit.point;
            Debug.DrawLine(shootPoint.position, mousePosition, Color.cyan);
            mousePosition.y = transform.position.y;//To avoid character tilt
            //Find the rotation desired
            Vector3 targetForward = mousePosition - transform.position;
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, targetForward, 400 * Time.deltaTime, 0);

            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            //Apply rotation
            transform.rotation = rotation;



            //QUERIA MOSTRAR LA TRAYECTORIA PERO NO ME HA SALIDO BIEN

            //Predict hit
            //ProjectileProperties data = new ProjectileProperties();
            //Projectile bullet = prefab.GetComponent<Projectile>();
            //Rigidbody rbBullet = bullet.GetComponent<Rigidbody>();

            //data.initialPosition = shootPoint.position;
            //data.initialSpeed = bullet.GetInitialSpeed();
            //data.direction = -shootPoint.up;
            //data.drag = rbBullet.drag;
            //data.mass = rbBullet.mass;
            //trajectoryPredictor.PredictTrajectory(data);

        }
        
    }
}
