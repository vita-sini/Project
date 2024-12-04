using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckingUpperBlock
{
    private List<GameObject> _topBlocks = new List<GameObject>(); // ������ ������� ������

    public CheckingUpperBlock()
    {
        UpdateTopBlock();
    }

    public bool IsBlockOnTop(GameObject block)
    {
        // ���������, �������� �� ���� ����� �� �������
        return _topBlocks.Contains(block);

    }

    public void UpdateTopBlock()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        _topBlocks.Clear();

        // ������� ������������ ������
        float maxHeight = GetMaxHeight();

        foreach (GameObject block in blocks)
        {
            // �������: ���� �� ������������ ������ � ������ ������
            if (IsNothingAbove(block) && Mathf.Abs(block.transform.position.y - maxHeight) < 0.1f) // ����������� ��� ��������� ������
            {
                _topBlocks.Add(block);
            }
        }

        Debug.Log("��������� ������� �����: " + _topBlocks.Count + " ������.");
    }

    private bool IsNothingAbove(GameObject block)
    {
        // ������ ����� (��������, BoxCollider)
        Collider collider = block.GetComponent<Collider>();
        if (collider == null) return true;

        Vector3 blockSize = collider.bounds.size;
        Vector3 overlapCenter = block.transform.position + Vector3.up * blockSize.y / 2; // ����� ������� ��� ������
        Vector3 overlapSize = new Vector3(blockSize.x * 0.9f, 0.1f, blockSize.z * 0.9f); // ��������� ������� ��� ������ (���� ������, ��� ��� ����)

        // ���������, ���� �� ���������� � ���� ����, ����� ������ �����
        Collider[] hits = Physics.OverlapBox(overlapCenter, overlapSize / 2, Quaternion.identity);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject != block) // ���� ��� �� ��� ����
            {
                return false; // ���� ������ ������
            }
        }

        return true; // ������ ������ ���
    }

    private float GetMaxHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        if (blocks.Length == 0) return 0f;

        // ������� ������������ ������ ����� ���� ������
        return blocks.Max(block => block.transform.position.y);
    }
}
