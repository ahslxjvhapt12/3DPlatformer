using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEditor.Search;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject titleTargetPos;

    [SerializeField] GameObject press2Start;
    [SerializeField] GameObject press2StartTargetPos;

    [SerializeField] GameObject jumpGauge;
    [SerializeField] PlayerMove pm;

    [SerializeField] CinemachineVirtualCamera introCam;

    [SerializeField] GameObject pause;
    [SerializeField] GameObject pauseBeforePos;
    [SerializeField] GameObject pauseAfterPos;
    [SerializeField] Button reset;

    [SerializeField] GameObject player;

    bool trigger = false;

    private void Awake()
    {
        Cursor.visible = false;
        SetScale();
    }

    private void SetScale()
    {
        Sequence seq = DOTween.Sequence()
            .Append(press2Start.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f))
            .Append(press2Start.transform.DOScale(new Vector3(1f, 1f, 1f), 1f))
            .OnComplete(() =>
            {
                if (press2Start.activeSelf)
                {
                    SetScale();
                }
            });
    }

    private void Update()
    {
        if (trigger)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                reset.interactable = true;
                pause.transform.DOMove(pauseAfterPos.transform.position, 1).SetUpdate(true);
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        trigger = true;
        Sequence seq = DOTween.Sequence()
            .Append(title.transform.DOMove(titleTargetPos.transform.position, 1f))
            .Join(press2Start.transform.DOMove(press2StartTargetPos.transform.position, 1f))
            .AppendInterval(0.5f)
            .OnComplete(() =>
            {
                pm.enabled = true;
                jumpGauge.SetActive(true);
                press2Start.SetActive(false);
                introCam.Priority = 9;
            });
    }

    public void ExitGame()
    {
        Debug.Log("Á¾·á");
        Application.Quit();
    }

    public void ResetPosition()
    {
        Time.timeScale = 1;
        Sequence seq = DOTween.Sequence()
            .AppendCallback(() =>
            {
                player.transform.GetComponent<PlayerMove>().enabled = false;
                player.transform.GetComponent<CharacterController>().enabled = false;
                player.transform.position = new Vector3(16f, 0f, 0f);
                Resume();
            })
            .AppendInterval(0.3f)
            .PrependCallback(() =>
            {
                player.transform.GetComponent<PlayerMove>().enabled = true;
                player.transform.GetComponent<CharacterController>().enabled = true;
            });
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Sequence seq = DOTween.Sequence()
            .Append(pause.transform.DOMove(pauseBeforePos.transform.position, 1).SetUpdate(true))
            .PrependCallback(() =>
            {
                reset.interactable = false;
            });
    }
}
