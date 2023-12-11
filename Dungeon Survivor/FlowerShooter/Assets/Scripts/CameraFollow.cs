using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Position")]
    [SerializeField]
    float lengthArm = 10;
    [SerializeField]
    float heightArm = 5;

    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothingSpeed = 3f;


    
    PlayerStates playerState; 
    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position -target.transform.forward * lengthArm +Vector3.up*heightArm;         
        playerState = target.GetComponent<PlayerStates>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Follow();
    }
    
    void Follow() 
    {
        //Just update position if we are in movement
        if(playerState.PlayerState == PlayerState.walking) 
        {
            Vector3 desiredPosition = target.position - target.transform.forward * lengthArm;
            desiredPosition.y = heightArm;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothingSpeed);        
        }

        //Always look the target
        transform.LookAt(target.position); 

    }
}
