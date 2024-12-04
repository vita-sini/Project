using UnityEngine;

public class CamRotate : MonoBehaviour
{
    // Скорость вращения камеры
    public float rotationSpeed = 50f;
    // Скорость перемещения камеры по оси Y (вверх/вниз при прокрутке колесика)
    public float scrollSpeed = 10f;

    // Ограничение по минимальной и максимальной высоте (по оси Y)
    public float minHeight = 2f;
    public float maxHeight = 20f;
    // Объект, вокруг которого будет вращаться камера
    public Transform target; // Сюда можно вставить объект, вокруг которого вращается камера

    private void Update()
    {
        // Если нажата клавиша Q, вращаем камеру влево
        if (Input.GetKey(KeyCode.Q))
        {
            RotateAroundTarget(-1);
        }

        // Если нажата клавиша E, вращаем камеру вправо
        if (Input.GetKey(KeyCode.E))
        {
            RotateAroundTarget(1);
        }

        // Перемещение камеры по оси Y при прокрутке колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            MoveCameraVertically(scroll);
        }
    }

    // Метод для вращения камеры вокруг цели
    private void RotateAroundTarget(float direction)
    {
        // Вращаем вокруг оси Y
        transform.RotateAround(target.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);
    }

    // Метод для перемещения камеры по оси Y (вверх/вниз)
    private void MoveCameraVertically(float scrollAmount)
    {
        // Получаем текущее положение камеры
        Vector3 position = transform.position;

        // Изменяем высоту камеры в зависимости от прокрутки колесика
        float newY = position.y + scrollAmount * scrollSpeed;

        // Ограничиваем высоту камеры в пределах minHeight и maxHeight
        newY = Mathf.Clamp(newY, minHeight, maxHeight);

        // Устанавливаем новое положение камеры
        position.y = newY;
        transform.position = position;
    }
}
