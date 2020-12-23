using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Globalization;

public class JugadorDisparo : MonoBehaviour
{
    public float vidaDisparo = 60;
    public int municionInicial = 50;

    protected int municionActual = 0;
    protected float puntuacion = 0;
    protected LineRenderer lineRender;
    protected JugadorVida jugadorVida;
    protected Text textPuntuacion;
    protected Text textPuntuacionFinal;
    protected Text textMunicion;
    protected Text textRecargar;

    // Use this for initialization
    void Start()
    {
        municionActual = municionInicial;
        lineRender = GetComponent<LineRenderer>();
        jugadorVida = GetComponentInParent<JugadorVida>();
        textPuntuacion = GameObject.Find("Text").GetComponent<Text>();
        textPuntuacionFinal = GameObject.Find("TextScore").GetComponent<Text>();
        textMunicion = GameObject.Find("MunicionTexto").GetComponent<Text>();
        textRecargar = GameObject.Find("RecargaTexto").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Fire1") == 1 && jugadorVida.vida > 0 && municionActual > 0)
        {
            GetComponent<ParticleSystem>().Play();

            //miro donde choraria el rayo desde la camara y me sirve de punto de destino para
            //sacar la direccion desde la pistola
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawRay(transform.position, (hitInfo.point - transform.position) * 100, Color.magenta);
                lineRender.enabled = true;
                lineRender.SetPosition(0, transform.position);
                lineRender.SetPosition(1, hitInfo.point);
                if (hitInfo.collider.gameObject.name.StartsWith("Zombunny"))
                {
                    EnemigoModelo enemigoModelo = hitInfo.collider.gameObject.GetComponent<EnemigoModelo>();
                    enemigoModelo.decrementarVida((int)(vidaDisparo * Time.deltaTime));

                    puntuacion += vidaDisparo * Time.deltaTime;
                    textPuntuacion.text = "Score: " + (int)puntuacion;
                    textPuntuacionFinal.text = "Score: " + (int)puntuacion;
                    
                    ParticleSystem particleSystem = hitInfo.collider.gameObject.GetComponentInChildren<ParticleSystem>();
                    particleSystem.Play();
                    //Debug.Log ("vida enemigo "+enemigoModelo.vida);
                }
                

                if (municionActual > 0)
                {
                    municionActual -= 1;
                    textMunicion.text = municionActual.ToString();
                }
            }
            //desactivo el rayo
            else
            {
                lineRender.enabled = false;
            }
        }
        else
        {
            lineRender.enabled = false;
            //Debug.Log ("No disparo");
        }

        if (municionActual == 0)
        {
            textRecargar.enabled = true;
        }

        if (Input.GetKey(KeyCode.R) && municionActual == 0)
        {
            municionActual = municionInicial;
            textMunicion.text = municionActual.ToString();
            textRecargar.enabled = false;
        }
    }
}