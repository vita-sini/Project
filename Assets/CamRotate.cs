using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // �������� �������� ������
    public float rotationSpeed = 50f;
    // �������� ����������� ������ �� ��� Y (�����/���� ��� ��������� ��������)
    public float scrollSpeed = 10f;

    // ����������� �� ����������� � ������������ ������ (�� ��� Y)
    public float minHeight = 2f;
    public float maxHeight = 20f;
    // ������, ������ �������� ����� ��������� ������
    public Transform target; // ���� ����� �������� ������, ������ �������� ��������� ������

    private void Update()
    {
        // ���� ������ ������� Q, ������� ������ �����
        if (Input.GetKey(KeyCode.Q))
        {
            RotateAroundTarget(-1);
        }

        // ���� ������ ������� E, ������� ������ ������
        if (Input.GetKey(KeyCode.E))
        {
            RotateAroundTarget(1);
        }

        // ����������� ������ �� ��� Y ��� ��������� �������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            MoveCameraVertically(scroll);
        }
    }

    // ����� ��� �������� ������ ������ ����
    private void RotateAroundTarget(float direction)
    {
        // ������� ������ ��� Y
        transform.RotateAround(target.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }

    // ����� ��� ����������� ������ �� ��� Y (�����/����)
    private void MoveCameraVertically(float scrollAmount)
    {
        // �������� ������� ��������� ������
        Vector3 position = transform.position;

        // �������� ������ ������ � ����������� �� ��������� ��������
        float newY = position.y + scrollAmount * scrollSpeed;

        // ������������ ������ ������ � �������� minHeight � maxHeight
        newY = Mathf.Clamp(newY, minHeight, maxHeight);

        // ������������� ����� ��������� ������
        position.y = newY;
        transform.position = position;
    }
}
