// Savor Util -  2017- Kim Sang Won
//
// Script handling transform with FadeIn FadeOut, and repeating... 

// Made by SavorK(Shabel@netsgo.com)


using UnityEngine;
using System.Collections;

public class Savor_MoveThis_Curve : MonoBehaviour
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
    public bool relativity = true;

    public Vector3 destPosition; 



    private float _pTime;

    private int _rep;
    private int _State;
    private AnimationCurve _NowCurve;


    private Transform _trans = null;
    private float delayTimer = 0.0f;
    private float playTimer = 0.0f;

    Vector3 _initTrans;
    
    Vector3 _destTrans;


    // Use this for initialization
    void Start()
    {

        _initTrans = this.transform.localPosition; ;

        if (relativity == true)
            _destTrans = _initTrans + destPosition;
        else
            _destTrans = destPosition;

    }

    void OnEnable()
    {

        _trans = GetComponent<Transform>();
        _rep = repeat;
        if (NoFI == true)
            _State = 1;
        else _State = 0;
        playTimer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {

        if (null == _trans)
            return; // Skip



        playTimer += Time.deltaTime;



        if (_State == 1)
            _pTime = playTime;
        else
            _pTime = fadeTime;


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

                MoveUp(playTimer);

                break;

            case 1:
                _NowCurve = MainCurve;

                MoveUp(playTimer);

                break;

            case 2:
                _NowCurve = FadeOutCurve;

                MoveUp(playTimer);

                break;

            default:

                break;

        }



    }


    void MoveUp(float f)
    {



        //_trans.localScale = initTrans * Mathf.Abs(_NowCurve.Evaluate(f / _pTime));

        // Debug.Log(playTimer);

        _trans.localPosition =
        Vector3.Lerp(_initTrans, _destTrans, (_NowCurve.Evaluate(f / _pTime)));


    }
}

