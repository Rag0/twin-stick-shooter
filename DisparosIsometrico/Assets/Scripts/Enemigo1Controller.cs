using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1Controller : MonoBehaviour
{
    public GameObject player;
    public float velocidad;
    public GameObject bulletEnemigo1;
    public GameObject murosMedio;

    private Rigidbody2D rb2d;
    private float movHorizontal;
    private float movVertical;


    public float fireDelta = 0.5F;
    public float siguienteDisparo;
    private float myTime = 0.0F;

    public static float movimientoActual;

    private bool movimientoLibre;

    // Use this for initialization
    void Start ()
    {
        movimientoLibre = false;

        movHorizontal = GameController.movHorizontalEnemigo;
        movVertical = GameController.movVerticalEnemigo;

        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (!GameController.gameOver)
        {
            if (movimientoLibre)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    movHorizontal = -1f;
                }

                else if (player.transform.position.x > transform.position.x)
                {
                    movHorizontal = 1f;

                }

                if (player.transform.position.y < transform.position.y)
                {
                    movVertical = -1f;
                }

                else if (player.transform.position.y > transform.position.y)
                {
                    movVertical = 1f;
                }
            }

            Vector2 movimiento = new Vector2(movHorizontal, movVertical);

            rb2d.AddForce(movimiento * velocidad);
        }
    }

    void Update()
    {
        if (!GameController.gameOver && movimientoLibre == true)
        {
            myTime = myTime + Time.deltaTime;

            if (myTime > siguienteDisparo)
            {
                siguienteDisparo = myTime + fireDelta;

                instanciarBalaEnemigo1();

                siguienteDisparo = siguienteDisparo - myTime;
                myTime = 0.0F;
            }
        }
    }

    void instanciarBalaEnemigo1()
    {
        //float diferenciaDistanciaX = Mathf.Abs(player.transform.position.y) - Mathf.Abs(gameObject.transform.position.y);
        //float diferenciaDistanciaY = Mathf.Abs(player.transform.position.x) - Mathf.Abs(gameObject.transform.position.x);

        float posXIzqPlayer = player.transform.position.x - (player.GetComponent<BoxCollider2D>().bounds.size.x / 2);
        float posXDerPlayer = player.transform.position.x + (player.GetComponent<BoxCollider2D>().bounds.size.x / 2);
        float posXIzqEnemigo = transform.position.x - (GetComponent<BoxCollider2D>().bounds.size.x / 2);
        float posXDerEnemigo = transform.position.x + (GetComponent<BoxCollider2D>().bounds.size.x / 2);

        float posYIzqPlayer = player.transform.position.y - (player.GetComponent<BoxCollider2D>().bounds.size.y / 2);
        float posYDerPlayer = player.transform.position.y + (player.GetComponent<BoxCollider2D>().bounds.size.y / 2);
        float posYIzqEnemigo = transform.position.y - (GetComponent<BoxCollider2D>().bounds.size.y / 2);
        float posYDerEnemigo = transform.position.y + (GetComponent<BoxCollider2D>().bounds.size.y / 2);

        if (posYDerPlayer >= posYIzqEnemigo && posYIzqPlayer <= posYDerEnemigo)
        {
            if (player.transform.position.x < transform.position.x)
            {
                movimientoActual = 1;
            }

            else
            {
                movimientoActual = 2;
            }

            Instantiate(bulletEnemigo1, transform.position, transform.rotation);
        }

        else if (posXDerPlayer >= posXIzqEnemigo && posXIzqPlayer <= posXDerEnemigo)
        {
            if (player.transform.position.y < transform.position.y)
            {
                movimientoActual = 3;
            }

            else
            {
                movimientoActual = 4;
            }

            Instantiate(bulletEnemigo1, transform.position, transform.rotation);
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MuroChoque")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "MuroMedio") 
		{
			movimientoLibre = true;

			Physics2D.IgnoreCollision(collider, GetComponent<Collider2D>());
		}
	}
}
