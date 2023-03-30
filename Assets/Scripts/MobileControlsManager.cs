using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileControlsManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CharacterMovementController _movementController;
    [SerializeField] private int _direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (_direction)
        {
            case 0:
                _movementController.SetMobileUp(true);
                break;
            case 1:
                _movementController.SetMobileUDown(true);
                break;
            case 2:
                _movementController.SetMobileRight(true);
                break;
            case 3:
                _movementController.SetMobileLeft(true);
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (_direction)
        {
            case 0:
                _movementController.SetMobileUp(false);
                break;
            case 1:
                _movementController.SetMobileUDown(false);
                break;
            case 2:
                _movementController.SetMobileRight(false);
                break;
            case 3:
                _movementController.SetMobileLeft(false);
                break;
        }
    }
}
