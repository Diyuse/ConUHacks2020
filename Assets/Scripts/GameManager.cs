using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Initializing,
        Starting,
        Playing,
        Over
    }

    // public GameObject enemy;

    private GameState currentState;
    // private List<GameObject> enemies;
    private float x;
    private float y;
    private int health;
    private int killCount;

    public GameState CurrentState
    {
        get { return currentState; }
    }

    public int Health
    {
        get { return health; }
    }

    public int KillCount
    {
        get { return killCount; }
    }

    public void SetGameState(GameState state)
    {
        currentState = state;
    }
    
    void Awake()
    {
        health = 10;
        killCount = 5;
        currentState = GameState.Initializing;
        // enemies = new List<GameObject>();
    }
    
    // public void SpawnEnemy(float row, float col, Transform pivot, int count)
    // {
    //     Vector3 pos = new Vector3(pivot.position.x - 1.0f, pivot.position.y, pivot.position.z + 0.2f);
    //     Instantiate(enemy, pos, pivot.rotation);
    //     for (int i = 1; i < count; i++)
    //     {
    //         x = Mathf.Clamp(i * 0.4f, -1.0f, 1.0f);
    //         y = Mathf.Clamp(i * 0.8f, 0.0f, 2.0f);
    //         pos = new Vector3(pivot.position.x + x, pivot.position.y - i*0.1f, pivot.position.z + y);
    //         Instantiate(enemy, pos, pivot.rotation);
    //     }
    // }

    public void Damage(int value)
    {
        health -= value;
    }

    public void EnemyKilled(int value)
    {
        killCount -= value;
    }
}
