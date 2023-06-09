using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpGaugeUI : MonoBehaviour
{
    [SerializeField] private GameObject bird;
    [SerializeField] private Slider slider;
    PlayerMove player;
    
    private Camera mainCam;

    float maxValue;

    private void Awake()
    {
        mainCam = Camera.main;
        player = bird.GetComponent<PlayerMove>();
        maxValue = player.maxAdditionalPower;
        slider.maxValue = maxValue;
    }

    private void Update()
    {
        transform.position = mainCam.WorldToScreenPoint(bird.transform.position);
        slider.value = player.extraPower;
    }
}
