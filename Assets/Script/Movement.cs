using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    private float _maxMoveSpeed = 5f; // Максимальная скорость перемещения
 
    private MouseWorldPosition _mouseWorldPosition;

    private Manipulation _manipulation;
    
    public Movement(MouseWorldPosition mouseWorldPosition, Manipulation manipulation)
    {
        _mouseWorldPosition = mouseWorldPosition;
        _manipulation = manipulation;
    }

    public void MoveMouse(Rigidbody selectedBlock, Vector3 offset, Plane movementPlane, Vector3 initialBlockPosition)
    {
        // Получаем новую позицию мыши с учётом смещения
        Vector3 newMousePosition = _mouseWorldPosition.GetMouseWorldPosition(movementPlane) + offset;

        // Вектор направления камеры "вправо" (горизонтальная ось)
        Vector3 cameraRight = Camera.main.transform.right;

        // Проекция движения на вектор "вправо"
        Vector3 horizontalMovement = Vector3.Project(newMousePosition - initialBlockPosition, cameraRight);

        // Добавляем проекцию к начальной позиции, чтобы ограничить движение только "вправо/влево"
        Vector3 targetPosition = initialBlockPosition + horizontalMovement;

        // Но оставляем текущую высоту (по оси Y) блока
        targetPosition.y = newMousePosition.y;

        // Рассчитываем желаемое направление и скорость
        Vector3 direction = targetPosition - selectedBlock.position;
        Vector3 velocity = direction / Time.fixedDeltaTime;

        // Ограничиваем максимальную скорость перемещения
        if (velocity.magnitude > _maxMoveSpeed)
        {
            velocity = velocity.normalized * _maxMoveSpeed;
        }

        // Применяем силу для перемещения блока
        selectedBlock.velocity = velocity;

        Debug.Log("Move");
    }
}
