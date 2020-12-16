using UnityEngine;
using System.Collections;
public class EnemigoModelo : MonoBehaviour
{
    public int vida { get; set; }
    public void decrementarVida(int valor)
    {
        vida -= valor;
        if (vida < 0)
            vida = 0;
    }
    // Use this for initialization
    void Awake()
    {
        vida = 100;
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
}