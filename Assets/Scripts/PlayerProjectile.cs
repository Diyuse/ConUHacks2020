using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameManager gm;
    private Vector3 direction;
    public float velocity;
    private PlayerController pc;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        Vector3 pos = new Vector3(FindClosestEnemy().transform.position.x, FindClosestEnemy().transform.position.y + 0.05f, FindClosestEnemy().transform.position.z);
        direction = pos - transform.position;
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * velocity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gm.EnemyKilled(1);
        } else if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
