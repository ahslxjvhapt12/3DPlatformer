using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 10f)] public float speed;      // 캐릭터 움직임 스피드.
    [Range(0, 10f)] public float jumpPower; // 캐릭터 점프 힘.
    [Range(0, 100f)] public float gravity;    // 캐릭터에게 작용하는 중력.

    [Range(0, 10f)] public float maxAdditionalPower;

    public float extraPower = 0;

    private CharacterController controller; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    [SerializeField] private Vector3 MoveDir = Vector3.zero; // 캐릭터의 움직이는 방향.
    [SerializeField] GameObject characterModel;

    [SerializeField] AudioSource audioSource;

    public bool flag = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        transform.position = new Vector3(PlayerPrefs.GetFloat("X", 16), PlayerPrefs.GetFloat("Y", -2), 0);
    }

    void Update()
    {
        flag = false;
        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded)
        {
            // 위, 아래 움직임 셋팅. 
            MoveDir = new Vector3(-Input.GetAxis("Horizontal"), 0, 0);

            MoveDir = transform.TransformDirection(MoveDir);
            // 스피드 증가.
            // 캐릭터 점프
            characterModel.transform.eulerAngles = new Vector3(0, MoveDir.x * 90, 0);

            MoveDir *= speed;
            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다
        }
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(transform.position, -transform.up, 1 * 0.125f, 1 << 7))
        {
            flag = true;
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log(1);
                extraPower += Time.deltaTime * 5;
                if (extraPower >= maxAdditionalPower)
                    extraPower = maxAdditionalPower;
            }
            //if (Input.GetMouseButtonUp(0))
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log(2);
                MoveDir.y = jumpPower + extraPower;
                audioSource?.Play();
                extraPower = 0;
            }
        }
        else
        {
            extraPower = 0;
        }

        MoveDir.y -= gravity * Time.deltaTime;
        controller.Move(MoveDir * Time.deltaTime);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("X", transform.position.x);
        PlayerPrefs.SetFloat("Y", transform.position.y);
    }

    private void OnDrawGizmosSelected()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);
    }
}