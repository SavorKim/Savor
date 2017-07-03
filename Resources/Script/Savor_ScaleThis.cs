// Savor Util -  2017- Kim Sang Won
//
// Script handling Object Scale with FadeIn FadeOut, repeating, and GetandDestroy... 

// Made by SavorK(Shabel@netsgo.com)


using UnityEngine;
using System.Collections;

public class Savor_ScaleThis : MonoBehaviour
{

    public AnimationCurve FadeInCurve;
    public AnimationCurve MainCurve;
    public AnimationCurve FadeOutCurve;
    public float fadeTime = 2.0f;
    public float playTime = 2.0f;
    
    //public float delayTime = 0.0f;
    public int repeat = 1;
    public float deadTime = 0.5f;

    public bool loop = false;
    public bool NoFI = false;

    private bool _GetStart = false;
    private float _pTime;

    private int _rep;
    private int _State;
    private AnimationCurve _NowCurve;


    private Transform _trans = null;
    private float delayTimer = 0.0f;
    private float playTimer = 0.0f;

    Vector3 StartScale;
    Vector3 _Scale;


    // Use this for initialization
    void Start()
    {


        StartScale = _trans.localScale;


    }

    void OnEnable()
    {

        _trans = GetComponent<Transform>();
        _rep = repeat;

        if (_GetStart == false)
        {
            if (NoFI == true)
                _State = 1;
            else _State = 0;
        }
        else _State = 2;

        playTimer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {

        if (null == _trans)
            return; // Skip

        if (_State == 3) {

                        if (_GetStart == true) {
                            StartCoroutine("GetAndDead"); //Get and Die!
                           }

                        else
                            return; // Skip
                         }

        playTimer += Time.deltaTime;



        if (_State == 1)
            _pTime = playTime;
        else
            _pTime = fadeTime;



        if (playTimer > _pTime)
        {
            if (loop == true )
            { _State = 1; }

            else if (_rep > 1)
            {
                _State = 1;
                _rep--;
            }

            else
            {   
                _State++;
            }


            playTimer = 0.0f;
        }



        switch (_State)
        {
            case 0:
                _NowCurve = FadeInCurve;

                ChangeScale(playTimer);

                break;

            case 1:
                _NowCurve = MainCurve;

                ChangeScale(playTimer);

                break;

            case 2:
                _NowCurve = FadeOutCurve;
                
                ChangeScale(playTimer);

                break;

            default:

                break;

        }



    }


    void ChangeScale(float f)
    {



        _trans.localScale = StartScale * Mathf.Abs(_NowCurve.Evaluate(f / _pTime));

        // Debug.Log(playTimer);
        
    }

    public void GetStart()
    {
        _GetStart = true;
        loop = false;
        _State = 2;
        
        
    }
    


    IEnumerator GetAndDead()
    {
        while (true)
        {
            yield return new WaitForSeconds(deadTime);
            
            {
                GameObject.Destroy(this.gameObject);
                break;
            }
        }
    }


}
