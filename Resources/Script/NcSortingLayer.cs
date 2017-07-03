/// Set Sorting Layer
/// Copyright (c) 2014 Tatsuhiko Yamamura
/// Released under the MIT license
// / http://opensource.org/licenses/mit-license.php

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NcSortingLayer : MonoBehaviour
{
    [SerializeField]
    private string
    _layerName = "Default";
    [SerializeField]
    private int
    _orderInLayer = 0;
    private Renderer _renderer;

    void Awake ()
    {
        LayerName = _layerName;
        OrderInLayer = _orderInLayer;
    }

    void OnValidate ()
    {
        LayerName = _layerName;
        OrderInLayer = _orderInLayer;
    }

    public string LayerName {
        get {
            return _layerName;
        }
        set {
            _layerName = value;
            foreach( Renderer renderer in GetComponents<Renderer>() )
            {
                renderer.sortingLayerName = _layerName;
            }
        }
    }

    public int OrderInLayer {
        get {
            return _orderInLayer;
        }
        set {
            _orderInLayer = value;
            foreach( Renderer renderer in GetComponents<Renderer>() )
            {
                renderer.sortingOrder = _orderInLayer;
            }
        }
    }
}

