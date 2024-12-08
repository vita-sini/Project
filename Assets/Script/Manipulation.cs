using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manipulation : MonoBehaviour
{
    private Pick _pick;
    private Release _release;
    private MouseWorldPosition _mouseWorldPosition;
    private Rotate _rotate;
    private Movement _movement;
    private CheckingUpperBlock _checkingUpperBlock;
    private ScoreUI _scoreUI;
    private Deck _deck;

    private Vector3 _offset; // Смещение мыши относительно центра блока
    private Plane _movementPlane; // Плоскость движения для блока
    private Rigidbody _selectedBlock;
    private Vector3 _initialBlockPosition;

    private void Awake()
    {
        _checkingUpperBlock = new CheckingUpperBlock();
        _mouseWorldPosition = new MouseWorldPosition();
        _pick = new Pick(_mouseWorldPosition, _checkingUpperBlock);
        _release = new Release();
        _rotate = new Rotate();
        _movement = new Movement(_mouseWorldPosition, this);
        _scoreUI = GameObject.FindObjectOfType<ScoreUI>();
        _deck = GameObject.FindObjectOfType<Deck>();
    }

    private void Update()
    {
        ClickLeftMouseButton();
        ClickRightMouseButton();
    }

    private void ClickLeftMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pick.Select(ref _selectedBlock, ref _offset, ref _movementPlane, ref _initialBlockPosition);
        }

        if (_selectedBlock != null && Input.GetMouseButton(0))
        {
            _movement.MoveMouse(_selectedBlock, _offset, _movementPlane, _initialBlockPosition);
            _rotate.Twist(_selectedBlock);
        }

        if (Input.GetMouseButtonUp(0) && _selectedBlock != null)
        {
            _release.FreeBlock(_selectedBlock);
            _checkingUpperBlock.UpdateTopBlock();
            // Запускаем корутину, чтобы дождаться остановки блока
            StartCoroutine(WaitForBlockToSettle(_selectedBlock, _initialBlockPosition));

            _selectedBlock = null;
        }
    }

    // Корутина для ожидания, пока блок перестанет двигаться
    private IEnumerator WaitForBlockToSettle(Rigidbody block, Vector3 initialPosition)
    {
        // Ждём, пока блок перестанет двигаться
        while (!_scoreUI.IsBlockAtRest(block))
        {
            yield return null; // Ждём один кадр
        }

        // Дополнительно ждём, пока блок не окажется на другом блоке
        ContactMonitor monitor = block.GetComponent<ContactMonitor>();

        while (!monitor.IsOnBlock())
        {
            yield return null; // Ждём один кадр
        }

        yield return new WaitForSeconds(1f);

        // Проверяем, был ли блок установлен выше начальной позиции
        if (block.transform.position.y > initialPosition.y + 1)
        {
            // Начисляем очки только если блок выше начальной позиции
            _scoreUI.CalculateScore(initialPosition, block.transform.position, block);
            _deck.OnTurnEnd();
        }
        else
        {
            Debug.Log("Блок не был установлен выше начальной позиции. Очки не начисляются.");
        }
    }

    private void ClickRightMouseButton()
    {

    }
}
