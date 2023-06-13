using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 6.0f;      // ĳ���� ������ ���ǵ�.
    public float jumpPower = 8.0f; // ĳ���� ���� ��.
    public float gravity = 20.0f;    // ĳ���Ϳ��� �ۿ��ϴ� �߷�.

    public float maxAdditionalPower = 3;

    public float extraPower = 0;

    private CharacterController controller; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�.
    private Vector3 MoveDir = Vector3.zero;                // ĳ������ �����̴� ����.
    [SerializeField] GameObject characterModel;


    public bool flag = false;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
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
            MoveDir *= speed;

            // ĳ���� ����
            characterModel.transform.eulerAngles = new Vector3(0, MoveDir.x * 90, 0);
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

    private void OnDrawGizmosSelected()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);
    }
}