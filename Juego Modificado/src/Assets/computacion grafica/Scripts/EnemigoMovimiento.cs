using UnityEngine;
using System.Collections;

public class EnemigoMovimiento : MonoBehaviour
{
    public GameObject jugador; //asignar gameobject del personaje en el inspector
    public float distanciaJugador = 10;
    protected EnemigoModelo enemigoModelo;
    protected UnityEngine.AI.NavMeshAgent navMeshAgent;
    protected Animator anim;
    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemigoModelo = GetComponent<EnemigoModelo>();
        anim = GetComponent<Animator>();
        if (jugador == null)
            jugador = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);
        //Debug.Log (distancia);

        if (distancia < distanciaJugador)
            anim.SetBool("cerca", true);
        else
            anim.SetBool("cerca", false);
        //Debug.Log ("vida "+enemigoModelo.vida);
        if (enemigoModelo.vida > 0 && distancia < distanciaJugador)
        {
            navMeshAgent.SetDestination(jugador.transform.position);
        }
        else if (enemigoModelo.vida <= 0) { navMeshAgent.Stop(); anim.SetBool("morir", true); }
    }
    //http://docs.unity3d.com/ScriptReference/AnimationEvent.html 
    void StartSinking(AnimationEvent animationEvent) {
        //Debug.Log ("llamo StartSinking "+animationEvent.floatParameter); 
        //elimino el gameobject pasado 2 segundos 
        Destroy(gameObject, 2);
    }
}