using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLightOff : MonoBehaviour
{
    [SerializeField] private GameObject globalLight;
    void Start()
    {
        globalLight.SetActive(false);
    }
}
