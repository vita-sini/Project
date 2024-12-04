using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public string blockTag = "Block"; // ���, ������� ������������ ��� ���� ������
    private HashSet<GameObject> baseBlocks = new HashSet<GameObject>(); // �����, ������� ��������� ����������

    private void OnCollisionEnter(Collision collision)
    {
        // ���������, ���� ������ ����� ��� "Block" � �������� �����
        if (collision.gameObject.CompareTag(blockTag))
        {
            // ��������� ���� � ������ ���������
            baseBlocks.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // ���� ���� ��������� �������� �����, ������� ��� �� ���������
        if (collision.gameObject.CompareTag(blockTag))
        {
            baseBlocks.Remove(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� ������ � ����� "Block" ���������� �������
        if (other.CompareTag(blockTag))
        {
            // ���� ���� �� �������� ������ ���������, ��������� ����
            if (!baseBlocks.Contains(other.gameObject))
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
    }
}
