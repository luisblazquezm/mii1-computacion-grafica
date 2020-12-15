using UnityEngine;
using System.Collections;

public class JugadorMovimiento : MonoBehaviour
{
	public float velocidad = 1;

	protected Animator anim;
	//protected JugadorVida jugadorVida;


	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		//jugadorVida = GetComponent<JugadorVida>();
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
			anim.SetBool("andando", true);
		}
		else
		{
			anim.SetBool("andando", false);
		}
	}
}
