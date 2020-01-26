using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public GameObject projectile;

    private int health;

    private GameObject player;

    private float count;

    private int limit;
    private GameManager gm;
    private ARTapToPlaceObject arTapToPlaceObject;

    // Start is called before the first frame update
    void Start()
    {
        arTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        gm = FindObjectOfType<GameManager>();
        player = GameObject.FindWithTag("Player");
        health = 2;
        count = 0;
        limit = 2;
    }

    // Update is called once per frame
    void Update()
    {
         if ((gm.CurrentState != GameManager.GameState.Over))
         {
            if (count >= limit)
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                Instantiate(projectile, pos, Quaternion.identity);
                count = 0;
            }
            else
            {
                count += Time.deltaTime;
            }
         }
    }
}
