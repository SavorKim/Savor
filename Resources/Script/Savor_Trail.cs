// Savor Util -  2017- Kim SangWon
//
// Script handling trail's width, time with FadeIn FadeOut, and repeating... 

// Made by SavorK(Shabel@netsgo.com)



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savor_Trail : MonoBehaviour {


    public AnimationCurve FadeInCurve;
    public AnimationCurve MainCurve;
    public AnimationCurve FadeOutCurve;
    // Curve for Time

    public AnimationCurve FadeInWCurve;
    public AnimationCurve MainWCurve;
    public AnimationCurve FadeOutWCurve;
    // Curve for Width

    public float fadeInTime = 2.0f;
    public float playTime = 2.0f;
    public float fadeOutTime = 2.0f;
    public float delayTime = 0.0f;

    public int repeat = 1;
    public float inBetween = 0.1f;
    public bool loop = false;
    public bool NoFI = false;

    public bool WidthAnimation = false;


    private TrailRenderer _trail;
    // private float _init_tAlpha;
    // private float _init_iAlpha;
    private float playTimer;
    private float _pTime;
    
   
    private int _rep;
    private int _State;

    private float _initTime;
    private float _initWidth;

    private float _tWidth;
    private bool _delay;

    private AnimationCurve _NowCurve; // Curve for Time
    private AnimationCurve _NowWCurve; // Curve for Width


    void Start()
    {
        _trail = GetComponent<TrailRenderer>();

        if (_trail){

            _initTime = _trail.time;
            _initWidth = _trail.widthMultiplier;
            
        }

        if (NoFI == true)
            _State = 1;
             else _State = 0;
              playTimer = 0.0f;

        if (delayTime > 0) _delay = true;

            StartCoroutine("TrailMove");

    }


    void OnEnable()
    {
        StopCoroutine("TrailMove");

        if (delayTime > 0) _delay = true;
        //_trail = GetComponent<TrailRenderer>();

        if (NoFI == true)
              _State = 1;
            else _State = 0;
               playTimer = 0.0f;


        StartCoroutine("TrailMove");

    }


    IEnumerator TrailMove()
    {

        while (_trail)
        {
            
            if(_delay) { 
                _trail.time = 0.0f;
                 yield return new WaitForSeconds(delayTime);
                _delay = false;
                _trail.time = _initTime;
            }

            yield return new WaitForSeconds(inBetween);

            playTimer += Time.deltaTime;



            if (_State == 1)
                _pTime = playTime;
            else if (_State == 0)
                _pTime = fadeInTime;
            else
                _pTime = fadeOutTime;


            if (playTimer > _pTime)
            {
                if (loop == true)
                { _State = 1; }

                else if (_rep > 1)
                {
                    _State = 1;
                    _rep--;
                }

                else
                { _State++; }



                playTimer = 0.0f;
            }


            switch (_State)
            {
                case 0:
                    _NowCurve = FadeInCurve;
                    _NowWCurve = FadeInWCurve;
                    if (_trail)
                    {
                        ChangeTrailTime(playTimer);
                        if (WidthAnimation)
                        {
                            ChangeTrailWidth(playTimer);
                        }
                    }

                    break;

                case 1:
                    _NowCurve = MainCurve;
                    _NowWCurve = MainWCurve;
                    if (_trail)
                    {
                        ChangeTrailTime(playTimer);
                        if (WidthAnimation)
                        {
                            ChangeTrailWidth(playTimer);
                        }
                    }

                    break;

                case 2:
                    _NowCurve = FadeOutCurve;
                    _NowWCurve = FadeOutWCurve;
                    if (_trail)
                    {
                        ChangeTrailTime(playTimer);
                        if (WidthAnimation)
                        {
                            ChangeTrailWidth(playTimer);
                        }
                    }

                    break;

                default:

                    break;

            }

        }

    }




    void ChangeTrailTime(float f)
    {


        //_tTime = _trail.time;
        _trail.time = _initTime * _NowCurve.Evaluate(f / _pTime);

        // Debug.Log(playTimer);

        //this.GetComponent<SpriteRenderer>().color = _Spr;

    }




    void ChangeTrailWidth(float f)
    {

        _tWidth = _initWidth * _NowWCurve.Evaluate(f / _pTime);
        this.GetComponent<TrailRenderer>().widthMultiplier = _tWidth;

    }

   


        

}
