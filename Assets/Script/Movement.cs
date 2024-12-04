using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private float _maxMoveSpeed = 5f; // ������������ �������� �����������
 
    private MouseWorldPosition _mouseWorldPosition;

    private Manipulation _manipulation;
    
    public Movement(MouseWorldPosition mouseWorldPosition, Manipulation manipulation)
    {
        _mouseWorldPosition = mouseWorldPosition;
        _manipulation = manipulation;
    }

    public void MoveMouse(Rigidbody selectedBlock, Vector3 offset, Plane movementPlane, Vector3 initialBlockPosition)
    {
        // �������� ����� ������� ���� � ������ ��������
        Vector3 newMousePosition = _mouseWorldPosition.GetMouseWorldPosition(movementPlane) + offset;

        // ������ ����������� ������ "������" (�������������� ���)
        Vector3 cameraRight = Camera.main.transform.right;

        // �������� �������� �� ������ "������"
        Vector3 horizontalMovement = Vector3.Project(newMousePosition - initialBlockPosition, cameraRight);

        // ��������� �������� � ��������� �������, ����� ���������� �������� ������ "������/�����"
        Vector3 targetPosition = initialBlockPosition + horizontalMovement;

        // �� ��������� ������� ������ (�� ��� Y) �����
        targetPosition.y = newMousePosition.y;

        // ������������ �������� ����������� � ��������
        Vector3 direction = targetPosition - selectedBlock.position;
        Vector3 velocity = direction / Time.fixedDeltaTime;

        // ������������ ������������ �������� �����������
        if (velocity.magnitude > _maxMoveSpeed)
        {
            velocity = velocity.normalized * _maxMoveSpeed;
        }

        // ��������� ���� ��� ����������� �����
        selectedBlock.velocity = velocity;

        Debug.Log("Move");
    }
}
