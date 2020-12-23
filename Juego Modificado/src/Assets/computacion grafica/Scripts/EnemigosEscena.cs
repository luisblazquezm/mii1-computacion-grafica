using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigosEscena : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public int intervaloCreacion = 5;
    public int instanteInicio = 2;
    public GameObject spawnEnemigo;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearEnemigo", 2, intervaloCreacion);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void crearEnemigo()
    {
        Instantiate(enemigoPrefab, spawnEnemigo.transform.position,
       Quaternion.identity);
    }

}
