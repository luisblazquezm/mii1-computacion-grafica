
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Reflection;

public class JugadorVida : MonoBehaviour
{
    public float vida = 200;
    public float energiaTotal = 200;
    protected float energia = 0;
    protected bool recuperarEnergia = false;
    public Image imagen; //pasar la barra de vida
    public Canvas canvas; //pasar el canvas de la puntuacion final
    public Text textoScore;
    public Image stamina; //pasar la barra de energía
    protected AudioSource[] audioSource;
    protected RectTransform rectTransform;
    protected RectTransform rectTransformStamina;
    protected Animator anim;
    protected ParticleSystem particleSystem;
    protected Vector2 vector2Vida;
    protected Vector2 vector2Stamina;
    // Use this for initialization
    void Start()
    {
        rectTransform = imagen.GetComponent<RectTransform>();
        rectTransformStamina = stamina.GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        audioSource = GetComponents<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        vector2Vida = new Vector2(vida, rectTransform.sizeDelta.y);
        energia = energiaTotal;
        vector2Stamina = new Vector2(energia, rectTransformStamina.sizeDelta.y);
    }
    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = vector2Vida;
        rectTransformStamina.sizeDelta = vector2Stamina;

        if (this.energia == 0)
            recuperarEnergia = true;

        if (this.energia < energiaTotal && recuperarEnergia)
        {
            this.energia += 20.0f;
            if (this.energia == energiaTotal)
                recuperarEnergia = false;
        }

    }

    public void restarVida(float vida)
    {
        if (this.vida <= 0)
            return;
        this.vida = this.vida - vida;
        vector2Vida = new Vector2(this.vida, rectTransform.sizeDelta.y);
        if (!audioSource[0].isPlaying)
            audioSource[0].Play();
        if (!particleSystem.isPlaying)
            particleSystem.Play();
        //Debug.Log ("Vida " + this.vida);
        //hacer lo que se estime oportuno para morir
        if (this.vida <= 0)
        {
            if (!audioSource[0].isPlaying)
                audioSource[0].Stop();
            audioSource[1].Play();
            anim.SetBool("morir", true);

            //activo la puntuacion final
            canvas.GetComponent<Canvas>().enabled = true;

            Invoke("gameOverEscena", 5);
        }
    }

    public void restarEnergia(float energia)
    {
        this.energia = this.energia - energia;
        vector2Stamina = new Vector2(this.energia, rectTransformStamina.sizeDelta.y);
    }

    //http://docs.unity3d.com/ScriptReference/Application.LoadLevel.html
    //añadir la escena en File->Build Settings
    void gameOverEscena()
    {
        Debug.Log ("Muero");
        SceneManager.LoadScene("gameover");
    }
}