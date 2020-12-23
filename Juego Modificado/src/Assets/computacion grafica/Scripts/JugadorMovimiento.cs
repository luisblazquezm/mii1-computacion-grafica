using UnityEngine;
using System.Collections;
public class JugadorMovimiento : MonoBehaviour
{
    public float velocidad = 1;
    private bool enSalto = false;
    private int impulso = 5;
    protected Animator anim; 
    protected JugadorVida jugadorVida;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        jugadorVida = GetComponent<JugadorVida>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (jugadorVida.vida > 0)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(KeyCode.Space) && enSalto == false)
            {
                salto();
            }

            movimiento(v);
            traslacion(v);
            rotacion(h);
        }
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
    }
    void rotacion(float horizontal)
    {
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 60, 0) * horizontal * velocidad * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(transform.rotation * deltaRotation);
    }
    void movimiento(float vertical)
    {
        // Cuando se pulsa "Mayus Izquierdo" se cambia la velocidad del personaje
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !enSalto)
        {
            velocidad = 10.0f;
        }
        else
        {
            velocidad = 1.0f;
        }

        if (vertical != 0)
        {
            anim.SetBool("andando", true);
        }
        else
        {
            anim.SetBool("andando", false);
        }
    }

    void salto()
    {
        enSalto = true;
        anim.SetBool("andando", false);
        GetComponent<Rigidbody>().AddForce(Vector2.up * impulso, ForceMode.Impulse);
        jugadorVida.restarEnergia(20.0f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("andando", true);
        enSalto = false;
    }
}