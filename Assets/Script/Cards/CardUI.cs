using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cardNameText;
    [SerializeField] private TextMeshProUGUI _cardDescriptionText;
    [SerializeField] private GameObject _cardPanel;
    [SerializeField] private Animator _cardAnimator;

    public void ShowCard(Card card)
    {
        _cardNameText.text = card.Name;
        _cardDescriptionText.text = card.Description;
        _cardPanel.SetActive(true);

        if (_cardAnimator != null)
        {
            _cardAnimator.SetTrigger("Show");
        }

        StartCoroutine(HideCardAfterDelay(3f));
    }

    private IEnumerator HideCardAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_cardAnimator != null)
        {
            _cardAnimator.SetTrigger("Hide");
            // Ждем окончания анимации
            yield return new WaitForSeconds(_cardAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        _cardPanel.SetActive(false);
    }
}
