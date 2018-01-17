using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad;
    public GameObject bullet;
    private Rigidbody2D rb2d;

	public float nextFire;
	public static float fireRate;

    public static float disparoActual;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        disparoActual = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        float movVertical = Input.GetAxis("Vertical");

        /*if (movHorizontal > 0)
        {
            disparoActual = 1;
        }

        else if (movHorizontal < 0)
        {
            disparoActual = 2;
        }

        if (movVertical > 0)
        {
            disparoActual = 3;
        }

        else if (movVertical < 0)
        {
            disparoActual = 4;
        }*/

        Vector2 movimiento = new Vector2(movHorizontal, movVertical);

        rb2d.AddForce(movimiento * velocidad);
    }

    void Update()
    {
        if (!GameController.gameOver)
        {
			if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
				SetDisparoYNextFire (4);
            }

			else if (Input.GetButton("Fire2") && Time.time > nextFire)
			{
				SetDisparoYNextFire (1);
			}

			else if (Input.GetButton("Fire3") && Time.time > nextFire)
			{
				SetDisparoYNextFire (2);
			}

			else if (Input.GetButton("Jump") && Time.time > nextFire)
			{
				SetDisparoYNextFire (3);
			}

			if (disparoActual > 0) 
			{
				Instantiate (bullet, transform.position, transform.rotation);
			}
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemigo1")
        {
            Destroy(gameObject);
            GameController.gameOver = true;
        }
    }

	private void SetDisparoYNextFire(float disparoActu)
	{
		nextFire = Time.time + fireRate;
		disparoActual = disparoActu;
	}
}
