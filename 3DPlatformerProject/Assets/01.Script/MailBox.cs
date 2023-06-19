using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MailBox : MonoBehaviour
{

    bool flag = false;
    [SerializeField] private Image image;

    private void Awake()
    {
        flag = false;
    }

    private void Update()
    {
        if (flag) return;
        Collider[] cols = Physics.OverlapSphere(transform.position + new Vector3(0, 1, 0), 0.5f, 1 << 6);
        if (cols.Length != 0)
        {
            Debug.Log("»õ µµÂø");
            flag = true;
            LoadClearScene();
        }
    }

    private void LoadClearScene()
    {
        Sequence seq = DOTween.Sequence()
            .Append(image.DOFade(1f, 1f))
            .OnComplete(() =>
            {
                PlayerPrefs.SetFloat("X", 16);
                PlayerPrefs.SetFloat("Y", -2);
                SceneManager.LoadScene("Clear");
            });
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1, 0), 0.5f);
    }
}
