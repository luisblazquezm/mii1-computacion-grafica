using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
	public float velocidad = 1;
	public int fuerzaSalto = 5;
	

	protected Animator anim;
	//protected JugadorVida jugadorVida;
	protected GameObject indicadorSalto;
	protected Rigidbody rb;

	private Vector3 movimientoSaltoJugador;
	private bool en_suelo;
	private float tiempoAndando;

	


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		//jugadorVida = GetComponent<JugadorVida>();
		indicadorSalto = GameObject.FindGameObjectWithTag("indicadorSalto");
		rb = GetComponent<Rigidbody>();
		en_suelo = true;
		tiempoAndando = 0.0f;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void FixedUpdate()
	{
		//if (jugadorVida.vida > 0)
		//{
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
		if (Input.GetKey(KeyCode.Space) && en_suelo == true)
		{
			salto();
		}
		
        
			movimiento(v);
			traslacion(v);
			rotacion(h);
		//}
	}

	void traslacion(float vertical)
	{
		Vector3 vector = new Vector3(0, 0, vertical);
		Vector3 desplazamiento;

		//para que el modulo sea uno y asi sea la misma velocidad siempre
		//vector = vector.normalized;
		desplazamiento = vector * velocidad * Time.deltaTime;
		desplazamiento = GetComponent<Rigidbody>().rotation * desplazamiento;
		GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().transform.position + desplazamiento);

		//alternativa sencilla
		//Vector3 vector = this.gameObject.transform.forward*Time.deltaTime*vertical;
		//GetComponent<Rigidbody>().MovePosition(transform.position + vector);
	}

	void rotacion(float horizontal)
	{
		Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 60, 0) * horizontal * velocidad * Time.deltaTime);
		GetComponent<Rigidbody>().MoveRotation(deltaRotation * transform.rotation);
	}


	void movimiento(float vertical)
	{
		if (vertical != 0)
		{
			anim.SetBool("Andar", true);
			tiempoAndando += Time.deltaTime;
			
			if (tiempoAndando > 3.0f && tiempoAndando < 4.0f)
            {
				velocidad = 1.5f;
            }
			if (tiempoAndando >= 4.0f && tiempoAndando < 7.0f)
            {

				velocidad = 2.0f;
            }
			if(tiempoAndando >= 7.0f && tiempoAndando < 15.0f)
            {

				velocidad = 2.5f;
            }
			if (tiempoAndando >=15.0f && tiempoAndando <25.0f)
			{

				velocidad = 3.0f;
			}
			if (tiempoAndando >= 25.0f)
			{

				velocidad = 5.0f;
			}


		}
		else
		{
			anim.SetBool("Andar", false);
			tiempoAndando = 0.0f;
			velocidad = 1.0f;
		}
	}

	void salto()
    {
		en_suelo = false;
		anim.SetBool("Saltar", true);
		anim.SetBool("Andar", false);
		GetComponent<Rigidbody>().AddForce(Vector2.up * fuerzaSalto,ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
			anim.SetBool("Saltar", false);
			anim.SetBool("Andar", false);
			en_suelo = true;
        }
    }
}
