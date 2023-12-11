using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    menu, play
}
public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static GameManager Instance { get => instance; }

    GameState state;
    public GameState State { get => state; }



    [SerializeField]
    PlayerHealthSystem playerHealth; //We can set position from here too

    [SerializeField]
    EnemySpawner enemySpawner;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject winMenu;

    [SerializeField]
    GameObject loseMenu;


    [SerializeField]
    Transform startPosition;
    // Start is called before the first frame update
    void Start()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        state = GameState.menu;
        playerHealth.transform.position = startPosition.position;

    }

    public void Win()
    {
        state = GameState.menu;
        winMenu.SetActive(true);
    }

    public void Lose()
    {
        state = GameState.menu;
        loseMenu.SetActive(true);
        enemySpawner.Restart();

    }

    public void Restart()
    {
        state = GameState.play;
        playerHealth.Restart();
        enemySpawner.Restart();
        playerHealth.transform.position = startPosition.position;
    }

    public void StartGame() 
    {
        enemySpawner.StartGame();
    }
    public GameState GetState()
    {
        return state;
    }
}
