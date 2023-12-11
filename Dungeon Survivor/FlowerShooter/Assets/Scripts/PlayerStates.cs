using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState
{
    idle, walking, jumping, attacking, hurt, dead
}
public class PlayerStates : MonoBehaviour
{

    PlayerState playerState;

    internal PlayerState PlayerState { get => playerState; set => playerState = value; }

    public void SetWalking() 
    {
        if(playerState == PlayerState.idle && playerState != PlayerState.walking)
            playerState = PlayerState.walking;

    
    }

    public void SetIdle() 
    {
        if(playerState != PlayerState.dead)
            playerState = PlayerState.idle;
    }

    public void SetJump() 
    {
        playerState = PlayerState.jumping;
    }

    public void SetAttacking() 
    {
        playerState = PlayerState.attacking; 
    }

    public void SetHurt()
    {
        playerState = PlayerState.hurt;
    }
}
