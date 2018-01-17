using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static int score;
	public Text scoreText;
	public Text gameOverText;
	public Text restartText;

	// Textos estadisticas
	public Text speedShot;
	public float fireRate;

    //Spawn this object
    public GameObject enemigo1;
    public static float movHorizontalEnemigo;
    public static float movVerticalEnemigo;

    public GameObject player1;

    public float maxTime = 8; 
    public float minTime = 4;

    //current time
    private float time;

    //The time to spawn the object
    private float spawnTime;

    public static bool gameOver;

    void Start()
    {
        gameOver = false;

        SetRandomTime();
        time = minTime;

        enemigo1.GetComponent<Enemigo1Controller>().player = player1;

		score = 0;
		fireRate = 0.5f;
		PlayerController.fireRate = fireRate;
		SetScore ();
		SetSpeedShot ();
    }

    void FixedUpdate()
    {
        if (!gameOver)
        {
			SetScore ();
			SetSpeedShot ();

            //Counts up
            time += Time.deltaTime;

            //Check if its the right time to spawn the object
            if (time >= spawnTime)
            {
                SpawnObject();
                SetRandomTime();
            }
        }
			
		else 
		{
			gameOverText.enabled = true;
			restartText.enabled = true;

			if (Input.GetButton ("Submit"))
			{
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
    }

	void SetScore()
	{
		scoreText.text = "Score: " + score;
	}

	void SetSpeedShot()
	{
		speedShot.text = "Speed Shot: " + fireRate;
	}

    //Spawns the object and resets the time
    void SpawnObject()
    {
        Vector3 posEnemigo = CalcularPosicionSpawn();
        time = 0;
        Instantiate(enemigo1, posEnemigo, enemigo1.transform.rotation);
    }

    //Sets the random time between minTime and maxTime
    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

    Vector3 CalcularPosicionSpawn()
    {
        float posX = 0f;
        float posY = 0f;
        float probabilidad = 0.5f;
        float numerRandom = Random.Range(0f, 1f);

        if (numerRandom <= probabilidad)
        {
            if (numerRandom <= 0.25f)
            {
                posX = 6f;

                movHorizontalEnemigo = -1f;
                movVerticalEnemigo = 0f;
            }

            else
            {
                posX = -6f;

                movHorizontalEnemigo = 1f;
                movVerticalEnemigo = 0f;
            }
        }

        else
        {
            if (numerRandom >= 0.75f)
            {
                posY = 6f;

                movHorizontalEnemigo = 0f;
                movVerticalEnemigo = -1f;
            }

            else
            {
                posY = -6f;

                movHorizontalEnemigo = 0f;
                movVerticalEnemigo = 1f;
            }
        }

        return new Vector3(posX, posY, 0f);
    }
}
