// Savor Util -  2017- Kim SangWon
//
// Script handling material color with FadeIn FadeOut, and repeating... 

// Made by SavorK(Shabel@netsgo.com)


using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
using System.Collections;



public class Savor_MaterialColor : MonoBehaviour
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

    public Color Color1;
    public Color Color2;

    private Color m_Color;
    private float m_playTimer;
    private float m_currentTime;
    private Renderer rend;
    
    private int m_repeatCount;
    private int m_currentState;
    private AnimationCurve m_currentCurve;



    void OnEnable()
    {
        rend = GetComponent<Renderer>();

        //rend.material.shader = Shader.Find("_TintColor");

        if (rend)
            rend.material.SetColor("_TintColor", Color1);

        m_repeatCount = repeat;
        if (NoFI == true)
            m_currentState = 1;
        else
            m_currentState = 0;
        m_playTimer = 0.0f;
    }


    void Update()
    {
        if (null == rend)
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
            SetColor(m_playTimer);
    }



    private void SetColor(float currentTime)
    {
        if (rend)
            ChangeColor(currentTime);

        
    }



    void ChangeColor(float currentTime)
    {
        

        //m_Color = rend.material.GetColor("Tint Color"); ;
        m_Color = Color.Lerp(Color1, Color2, m_currentCurve.Evaluate(currentTime / m_currentTime));
        rend.material.SetColor("_TintColor", m_Color);
        // Debug.Log(playTimer);     

    }




}