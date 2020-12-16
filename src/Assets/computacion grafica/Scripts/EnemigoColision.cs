using UnityEngine;
using System.Collections;
public class EnemigoColision : MonoBehaviour
{
    protected EnemigoModelo enemigoModelo;
    // Use this for initialization
    void Start()
    {
        enemigoModelo = GetComponent<EnemigoModelo>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    //OnTriggerEnter solo se ejecuta cuando se tocan pero si se siguen tocando ya no
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && enemigoModelo.vida > 0)
        {
            JugadorVida jugadorVida = collider.gameObject.GetComponent<JugadorVida>();
            jugadorVida.restarVida(100 * Time.deltaTime);
        }
    }
}