using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Action<Vector3Int> onMouseClick, onMouseHold;
    public Action onMouseUp;

    private Vector2 cameraMovementVector;

    [SerializeField]
    Camera mainCamera;

    public LayerMask groundMask;

    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }


    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
    }


    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }
        return null;
    }

    private void CheckArrowInput()
    {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    private void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if(position != null)
            {
                onMouseHold?.Invoke(position.Value);
            }
        }
    }

    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
                onMouseUp?.Invoke();
        }
    }

    private void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
          
            var position = RaycastGround();
            if (position != null)
            {
                onMouseClick?.Invoke(position.Value);
            }
        }
    }
}
