using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI _scoreText; // Текстовый элемент для отображения очков
    private int _currentScore = 0;

    public void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        UpdateScoreText();
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + _currentScore;
        }
    }

    // Метод для проверки, лежит ли блок в покое (скорость близка к нулю)
    public bool IsBlockAtRest(Rigidbody block)
    {
        Debug.Log("block.velocity.magnitude" + " " + block.velocity.magnitude);
        return block.velocity.magnitude < 0.01f && block.angularVelocity.magnitude < 0.01f;
    }

    public void CalculateScore(Vector3 initialPosition, Vector3 currentPosition, Rigidbody block)
    {
        // Если блок ниже начальной позиции, очки не начисляются
        if (currentPosition.y <= initialPosition.y + 1)
        {
            Debug.Log("Блок опущен ниже или не перемещён, очки не начисляются.");
            return;
        }

        // Если все условия выполнены, начисляем очки
        AddScore(10);
        Debug.Log("Очки начислены за успешное перемещение блока.");
    }
}
