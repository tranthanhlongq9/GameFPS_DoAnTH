using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMap : MonoBehaviour
{
    public RawImage map;
    private bool isMapEnabled;

    private void Awake()
    {
        isMapEnabled = false;
        map.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapEnabled = !isMapEnabled;
            MapEnabling();
        }
    }
    private void MapEnabling()
    {
        map.enabled = isMapEnabled;
    }
}
