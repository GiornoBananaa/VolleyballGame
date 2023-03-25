using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopUpOn : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void CallPopUp()
    {
        canvas.enabled = true;
    }
}
