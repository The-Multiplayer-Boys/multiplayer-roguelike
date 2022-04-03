using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightOff : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Global Light 2D").SetActive(false);
    }
}
