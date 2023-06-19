using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ClearScene : MonoBehaviour
{

    [SerializeField] Image image;
    TextMeshProUGUI text;
    private void Awake()
    {
        image.DOFade(0f, 1f);
    }

    public void Return()
    {
        SceneManager.LoadScene("MainScene");
    }
}
