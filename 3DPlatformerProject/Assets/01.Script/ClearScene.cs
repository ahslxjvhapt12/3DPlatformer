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
    
    private void Awake()
    {
        Cursor.visible = true;
        image.DOFade(0f, 1f);
    }

    public void Return()
    {
        SceneManager.LoadScene("MainScene");
    }
}
