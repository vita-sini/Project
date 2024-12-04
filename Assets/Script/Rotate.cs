using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate
{
    public float _rotationAnglePositive = 10f;
    public float _rotationAngleNegative = -10f;

    public void Twist(Rigidbody selectedBlock)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // ������������ ���� �� ������������ ���� �����
            selectedBlock.MoveRotation(selectedBlock.rotation * Quaternion.Euler(0, _rotationAnglePositive, 0));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            // ������������ ���� �� ������������ ���� ������
            selectedBlock.MoveRotation(selectedBlock.rotation * Quaternion.Euler(0, _rotationAngleNegative, 0));
        }
    }
}
