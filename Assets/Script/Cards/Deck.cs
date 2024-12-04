using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Deck : MonoBehaviour
{
    private Random _random = new Random();
    private List<Card> _cards = new List<Card>();
    private int _turnCounter = 0;
    [SerializeField] private int _turnsUntilNextCard = 3;
    [SerializeField] private CardUI _cardUI;
    private Effects _effects;

    private void Awake()
    {
        _effects = GetComponent<Effects>();
        InitializeDeck();
        ShuffleDeck();
    }

    private void InitializeDeck()
    {
        // Добавляем карты в колоду
        _cards.Add(new WindCard(_effects));
        _cards.Add(new EarthquakeCard(_effects));
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            int randomPosition = _random.Next(_cards.Count);
            Card temporary = _cards[randomPosition];
            _cards[randomPosition] = _cards[i];
            _cards[i] = temporary;
        }
    }

    public void OnTurnEnd()
    {
        _turnCounter++;
        if (_turnCounter >= _turnsUntilNextCard)
        {
            DrawCard();
            _turnCounter = 0;
        }
    }

    private void DrawCard()
    {
        if (_cards.Count > 0)
        {
            Card drawnCard = _cards[0];
            _cards.RemoveAt(0);

            // Показываем карту в UI
            _cardUI.ShowCard(drawnCard);

            // Выполняем эффект карты
            drawnCard.Execute();

            // Возвращаем карту в колоду
            _cards.Add(drawnCard);
            ShuffleDeck();
        }
    }
}
