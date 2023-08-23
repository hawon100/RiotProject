using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarFollow : MonoBehaviour
{
    public GameObject _hpbar;
    GameObject _obj;

    Canvas _canvas;
    Camera _cam;

    private void Awake()
    {
        _obj = this.transform.parent.gameObject;
        _cam = Camera.main;
    }

    private void Update()
    {
        _hpbar.transform.position = _cam.WorldToScreenPoint(_obj.transform.position + new Vector3(0f, 2.5f, 0f));
    }
}
