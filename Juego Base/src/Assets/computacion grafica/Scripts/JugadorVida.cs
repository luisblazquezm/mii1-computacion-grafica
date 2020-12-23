
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class JugadorVida : MonoBehaviour
{
    public float vida = 200;
    public Image imagen; //pasar la barra de vida
    public Canvas canvas; //pasar el canvas de la puntuacion final
    public Text textoScore;
    protected AudioSource[] audioSource;
    protected RectTransform rectTransform;
    protected Animator anim;
    protected ParticleSystem particleSystem;
    protected Vector2 vector2Vida;
    // Use this for initialization
    void Start()
    {
        rectTransform = imagen.GetComponent<RectTransform>();
        anim = GetComponent<Animator>();
        audioSource = GetComponents<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        vector2Vida = new Vector2(vida, rectTransform.sizeDelta.y);
    }
    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = vector2Vida;
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

    //http://docs.unity3d.com/ScriptReference/Application.LoadLevel.html
    //añadir la escena en File->Build Settings
    void gameOverEscena()
    {
        Debug.Log ("Muero");
        SceneManager.LoadScene("gameover");
    }
}