using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    public float velocidadBala;
    private float disparoActual;

	public int puntajeEnemigo;

    // Use this for initialization
    void Start ()
    {
        disparoActual = PlayerController.disparoActual;
		PlayerController.disparoActual = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!GameController.gameOver)
        {
            float movX = transform.position.x;
            float movY = transform.position.y;

            if (disparoActual == 1)
            {
                movX = movX + velocidadBala;
            }

            else if (disparoActual == 2)
            {
                movX = movX - velocidadBala;
            }

            else if (disparoActual == 3)
            {
                movY = movY + velocidadBala;
            }

            else if (disparoActual == 4)
            {
                movY = movY - velocidadBala;
            }

            transform.position = new Vector3(movX, movY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "BalaEnemigo")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemigo1")
        {
            Destroy(other.gameObject);

			GameController.score = GameController.score + puntajeEnemigo;
        }
    }
}
