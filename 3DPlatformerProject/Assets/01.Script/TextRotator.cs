using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotator : MonoBehaviour
{
    public List<GameObject> texts = new List<GameObject>();
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        for (int i = 0; i < texts.Count; ++i)
        {
            texts[i].transform.rotation = mainCam.transform.rotation;
        }
    }
}
