using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<Transform> spawnerPoints;
    [SerializeField]
    Transform target;

    [SerializeField]
    GameObject[] enemiesPrefabs;
    List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();

    
    float maxRound = 15;
    [SerializeField]
    private float easyModeRounds;
    [SerializeField]
    private float hardModeRounds;

    [SerializeField]
    float timeBetweenRounds = 7;
    float round;
    int enemyPerRound = 1;

    float timer = 0;

    

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.GetState() == GameState.play)
        {
            timer += Time.deltaTime;
            if(timer>= timeBetweenRounds)
            {
                NextRound();
                timer = 0;
            }

            if(enemies.Count == 0 && round >= maxRound)
            {
                GameManager.Instance.Win();
            }

        }
    }
    public void StartGame() 
    {
        Invoke("NextRound", 2);
    }
    void NextRound() 
    {
        round++;

        if (round <= maxRound)
        {
            if (round == Mathf.RoundToInt(maxRound / 3) && maxRound>10)//Round To Int to avoid decimals in round && i check if there are so many rounds to make more difficult
                enemyPerRound++;
            else if (round == Mathf.RoundToInt(maxRound / 2))
                enemyPerRound++;

            SpawnEnemy();
            

        }
       


    }
    void SpawnEnemy()
    {
        List<Transform> avaliblePoints = new List<Transform>(spawnerPoints);
        
        for(int i = 0; i < enemyPerRound; i++) 
        {
            int prefabRandom = Random.Range(0, enemiesPrefabs.Length);
            int spawnPointRandom = Random.Range(0, avaliblePoints.Count);
            EnemyBehaviour enemy =  Instantiate(enemiesPrefabs[prefabRandom], avaliblePoints[spawnPointRandom].position, Quaternion.identity).GetComponent<EnemyBehaviour>();
            enemy.SetTarget(target);
            enemy.SetSpawner(this);

            enemies.Add(enemy);
            avaliblePoints.Remove(avaliblePoints[spawnPointRandom]);//Dont spawn 2 enemies in the same point
        
        }
    }

    public void RemoveEnemy(EnemyBehaviour enemy) 
    {
        enemies.Remove(enemy);
    }
    public void Restart()
    {
        foreach(EnemyBehaviour enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        
        enemies.Clear();
        round = 0;
        enemyPerRound = 1;
        timer = 0;
        
    }

    public void SetDificulty(bool hard) 
    {
        maxRound = hard? hardModeRounds : easyModeRounds;
    }
}
