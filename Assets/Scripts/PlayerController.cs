using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    public GameObject indicator;
    public int movementSpeed;
    public TextMeshProUGUI action;
    public TextMeshProUGUI killCount;
    public TextMeshProUGUI health;
    public Slider healthBar;
    public GameObject graphics;

    private Vector3 difference;
    private Pose pose;
    private ARTapToPlaceObject arTapToPlaceObject;
    private GameManager gm;
    private bool reset;
    public GameObject target;

    private void Start()
    {
        arTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        gm = FindObjectOfType<GameManager>();
        graphics.SetActive(false);
        reset = false;
    }

    void Update()
    {
        if (gm.CurrentState == GameManager.GameState.Initializing)
        {
            killCount.text = "";
            health.text = "";
            healthBar.gameObject.SetActive(false);
        } else if (gm.CurrentState == GameManager.GameState.Starting)
        {
            action.text = "";
            healthBar.gameObject.SetActive(true);
            graphics.SetActive(true);
            gm.SetGameState(GameManager.GameState.Playing);
            // gm.SpawnEnemy(2.4f, 2.4f, indicator.transform, 5);
        }else if (gm.CurrentState == GameManager.GameState.Playing)
        {
            pose = arTapToPlaceObject.Pose;
            difference = (indicator.transform.position - transform.position); // Vector that points from the player to the indicator
            transform.Translate(difference * movementSpeed * Time.deltaTime); // Move the player using that vector
            transform.rotation = pose.rotation; // Rotate player with the camera

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 pos = new Vector3(pose.position.x, pose.position.y + 0.05f, pose.position.z);
                Instantiate(projectile, pos, Quaternion.identity);
            }
            
            killCount.text = String.Format("Enemies Left: {0}", gm.KillCount);
            health.text = String.Format("{0}/10", gm.Health);
            healthBar.value = gm.Health / 10f;

            if (gm.Health < 1)
            {
                action.text = "You Lose!";
                gm.SetGameState(GameManager.GameState.Over);
            } else if (gm.KillCount < 1)
            {
                action.text = "You Win!";
                gm.SetGameState(GameManager.GameState.Over);
            }
        } else if (gm.CurrentState == GameManager.GameState.Over)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !reset)
            {
                action.text = "Tap again to reset!";
                reset = true;
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && reset)
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }
}
