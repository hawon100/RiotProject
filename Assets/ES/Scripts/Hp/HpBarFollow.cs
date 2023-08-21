using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarFollow : MonoBehaviour
{
    public GameObject _hpbar;

    GameObject _obj;

    Camera m_cam = null;

    private void Start()
    {
        m_cam = Camera.main;

        GameObject t_object = GameObject.FindGameObjectWithTag("Enemy");
        _obj = t_object;
        _hpbar.transform.position = t_object.transform.position;
        _hpbar.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        _hpbar.transform.position = m_cam.WorldToScreenPoint(_obj.transform.position + new Vector3(0f, 2.5f, 0f));
    }
}
