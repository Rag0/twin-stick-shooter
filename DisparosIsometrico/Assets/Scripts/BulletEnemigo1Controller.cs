using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemigo1Controller : MonoBehaviour
{
    public float velocidadBala;
    private float movActualBalaEnem;
    // Use this for initialization
    void Start()
    {
        movActualBalaEnem = Enemigo1Controller.movimientoActual;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameOver)
        {
            float movX = transform.position.x;
            float movY = transform.position.y;

            if (movActualBalaEnem == 1)
            {
                movX = movX - velocidadBala;
            }

            else if (movActualBalaEnem == 2)
            {
                movX = movX + velocidadBala;
            }

            else if (movActualBalaEnem == 3)
            {
                movY = movY - velocidadBala;
            }

            else if (movActualBalaEnem == 4)
            {
                movY = movY + velocidadBala;
            }

            transform.position = new Vector3(movX, movY, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            GameController.gameOver = true;
        }

		if (other.gameObject.tag != "Enemigo1" && other.gameObject.tag != "BalaPlayer")
        {
            Destroy(gameObject);
        }
    }
}
