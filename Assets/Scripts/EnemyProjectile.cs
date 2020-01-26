using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameManager gm;
    private GameObject player;
    private Vector3 direction;
    public float velocity;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");
        direction = player.transform.position - transform.position;
        
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gm.Damage(1);
        } else if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    
}

