using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Range(0, 10f)] public float speed;      // ĳ���� ������ ���ǵ�.
    [Range(0, 10f)] public float jumpPower; // ĳ���� ���� ��.
    [Range(0, 100f)] public float gravity;    // ĳ���Ϳ��� �ۿ��ϴ� �߷�.

    [Range(0, 10f)] public float maxAdditionalPower;

    public float extraPower = 0;

    private CharacterController controller; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�.
    [SerializeField] private Vector3 MoveDir = Vector3.zero; // ĳ������ �����̴� ����.
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
        // ���� ĳ���Ͱ� ���� �ִ°�?
        if (controller.isGrounded)
        {
            // ��, �Ʒ� ������ ����. 
            MoveDir = new Vector3(-Input.GetAxis("Horizontal"), 0, 0);

            MoveDir = transform.TransformDirection(MoveDir);
            // ���ǵ� ����.
            // ĳ���� ����
            characterModel.transform.eulerAngles = new Vector3(0, MoveDir.x * 90, 0);

            MoveDir *= speed;
            // ���͸� ���� ��ǥ�� ���ؿ��� ���� ��ǥ�� �������� ��ȯ�Ѵ�
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