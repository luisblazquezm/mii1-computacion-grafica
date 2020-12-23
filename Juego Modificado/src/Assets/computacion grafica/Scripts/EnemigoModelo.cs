using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Reflection;

public class EnemigoModelo : MonoBehaviour
{
    public int vida { get; set; }
    private Vector2 vector2Vida;
    public RectTransform rectTransform;

    public void decrementarVida(int valor)
    {
        vida -= valor;
        if (vida < 0)
            vida = 0;

        this.vector2Vida = new Vector2(this.vida, this.rectTransform.sizeDelta.y);
    }
    // Use this for initialization
    void Awake()
    {
        vida = 100;
        this.vector2Vida = new Vector2(this.vida, this.rectTransform.sizeDelta.y);
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        this.rectTransform.sizeDelta = this.vector2Vida;
    }
}