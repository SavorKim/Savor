// Savor Util -  2017- Kim SangWon
//
// Script handling sprite alpha, UI Image, UI Text, TextMeshPro(2017.04!), and Lens flares with FadeIn FadeOut, and repeating... 

// Made by SavorK(Shabel@netsgo.com)


using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;



public class Savor_SpriteAlpha : MonoBehaviour
{

    public AnimationCurve FadeInCurve;
    public AnimationCurve MainCurve;
    public AnimationCurve FadeOutCurve;
    public float fadeTime = 2.0f;
    public float playTime = 2.0f;
    //public float delayTime = 0.0f;
    public int repeat = 1;
    public bool loop = false;
    public bool NoFI = false;


    private Color m_spriteColor;
    // private float _init_tAlpha;
    // private float _init_iAlpha;
    private float m_playTimer;
    private float m_currentTime;
    private SpriteRenderer m_curSpriteRenderer;
    private TextMeshPro m_TextMeshPro;
    private Image m_image;
    private Text m_text;
    private LensFlare m_lensFlare;
    private float m_originalBrightness;
    private int m_repeatCount;
    private int m_currentState;
    private AnimationCurve m_currentCurve;

    private void Start()
    {
        m_lensFlare = GetComponent<LensFlare>();
        if (m_lensFlare)
            m_originalBrightness = m_lensFlare.brightness;
    }

    void OnEnable()
    {

        m_curSpriteRenderer = this.GetComponent<SpriteRenderer>();
        //_init_tAlpha = t.color.a;

        m_image = GetComponent<Image>();
        //_init_iAlpha = image.color.a;

        m_text = GetComponent<Text>();


        m_TextMeshPro = GetComponent<TextMeshPro>();


        m_repeatCount = repeat;
        if (NoFI == true)
            m_currentState = 1;
        else
            m_currentState = 0;
        m_playTimer = 0.0f;
    }


    void Update()
    {
        if (null == m_curSpriteRenderer && null == m_image && null == m_text && null == m_lensFlare && null == m_TextMeshPro)
            return; // Skip



        m_playTimer += Time.deltaTime;



        if (m_currentState == 1)
            m_currentTime = playTime;
        else
            m_currentTime = fadeTime;


        if (m_playTimer > m_currentTime) {
            if (loop == true) {
                m_currentState = 1;
            } else if (m_repeatCount > 1) {
                m_currentState = 1;
                m_repeatCount--;
            } else {
                m_currentState++;
            }



            m_playTimer = 0.0f;
        }


        switch (m_currentState) {
        case 0:
            m_currentCurve = FadeInCurve;
            break;
        case 1:
            m_currentCurve = MainCurve;
            break;
        case 2:
            m_currentCurve = FadeOutCurve;
            break;
        default:
            break;
        }

        if (m_currentState < 3) // for state 3 -> all animation ended!
            SetAlpha(m_playTimer);
    }

    public void ReInitialize(bool isVisible = true)
    {
        m_currentCurve = FadeInCurve;
        enabled = false;
        m_currentTime = isVisible ? 1f : 0f;
        SetAlpha(isVisible ? 1f : 0f);
    }

    private void SetAlpha(float currentTime)
    {
        if (m_curSpriteRenderer)
            ChangeSpriteAlpha(currentTime);

        if (m_image)
            ChangeImageAlpha(currentTime);

        if (m_text)
            ChangeTextAlpha(currentTime);

        if (m_lensFlare)
            ChangeLensFlareAlpha(currentTime);

        if (m_TextMeshPro)
            ChangeTextMeshProAlpha(currentTime);
        
    }



    void ChangeSpriteAlpha(float currentTime)
    {


        m_spriteColor = m_curSpriteRenderer.color;
        m_spriteColor.a = Mathf.Lerp(0.0f, 1.0f, m_currentCurve.Evaluate(currentTime / m_currentTime));

        // Debug.Log(playTimer);

        this.GetComponent<SpriteRenderer>().color = m_spriteColor;

    }




    void ChangeImageAlpha(float currentTime)
    {


        m_spriteColor = m_image.color;
        m_spriteColor.a = Mathf.Lerp(0.0f, 1.0f, m_currentCurve.Evaluate(currentTime / m_currentTime));

        // Debug.Log(playTimer);

        this.GetComponent<Image>().color = m_spriteColor;

    }

    void ChangeTextAlpha(float currentTime)
    {


        m_spriteColor = m_text.color;
        m_spriteColor.a = Mathf.Lerp(0.0f, 1.0f, m_currentCurve.Evaluate(currentTime / m_currentTime));

        // Debug.Log(playTimer);

        this.GetComponent<Text>().color = m_spriteColor;

    }

    void ChangeLensFlareAlpha(float currentTime)
    {


        //_Bri = lF.brightness;
        float _Bri = m_currentCurve.Evaluate(currentTime / m_currentTime);

        // Debug.Log(playTimer);

        m_lensFlare.brightness = _Bri * m_originalBrightness;

    }


    void ChangeTextMeshProAlpha(float currentTime)
    {


        m_spriteColor = m_TextMeshPro.color;
        m_spriteColor.a = Mathf.Lerp(0.0f, 1.0f, m_currentCurve.Evaluate(currentTime / m_currentTime));

        // Debug.Log(playTimer);

        this.GetComponent<TextMeshPro>().color = m_spriteColor;

    }

    /*
        IEnumerator WaitTime(float dTime)
        {

            yield return new WaitForSeconds(dTime);

        }

        */

}