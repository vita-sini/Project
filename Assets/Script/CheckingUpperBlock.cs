using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckingUpperBlock
{
    private List<GameObject> _topBlocks = new List<GameObject>(); // Список верхних блоков

    public CheckingUpperBlock()
    {
        UpdateTopBlock();
    }

    public bool IsBlockOnTop(GameObject block)
    {
        // Проверяем, является ли блок одним из верхних
        return _topBlocks.Contains(block);

    }

    public void UpdateTopBlock()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        _topBlocks.Clear();

        // Находим максимальную высоту
        float maxHeight = GetMaxHeight();

        foreach (GameObject block in blocks)
        {
            // Условие: блок на максимальной высоте и ничего сверху
            if (IsNothingAbove(block) && Mathf.Abs(block.transform.position.y - maxHeight) < 0.1f) // Погрешность для сравнения высоты
            {
                _topBlocks.Add(block);
            }
        }

        Debug.Log("Обновлены верхние блоки: " + _topBlocks.Count + " блоков.");
    }

    private bool IsNothingAbove(GameObject block)
    {
        // Размер блока (например, BoxCollider)
        Collider collider = block.GetComponent<Collider>();
        if (collider == null) return true;

        Vector3 blockSize = collider.bounds.size;
        Vector3 overlapCenter = block.transform.position + Vector3.up * blockSize.y / 2; // Центр области над блоком
        Vector3 overlapSize = new Vector3(blockSize.x * 0.9f, 0.1f, blockSize.z * 0.9f); // Небольшая область над блоком (чуть меньше, чем сам блок)

        // Проверяем, есть ли коллайдеры в этой зоне, кроме самого блока
        Collider[] hits = Physics.OverlapBox(overlapCenter, overlapSize / 2, Quaternion.identity);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject != block) // Если это не сам блок
            {
                return false; // Есть объект сверху
            }
        }

        return true; // Сверху ничего нет
    }

    private float GetMaxHeight()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        if (blocks.Length == 0) return 0f;

        // Находим максимальную высоту среди всех блоков
        return blocks.Max(block => block.transform.position.y);
    }
}
