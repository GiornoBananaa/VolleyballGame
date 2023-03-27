using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMsingleUP : MonoBehaviour
{
    [SerializeField] private GameObject[] _elements;
    [SerializeField] private Button _button;
    [SerializeField] private Sprite _spriteDeafult;
    [SerializeField] private Sprite _spriteOn;

    private bool _isOn;

    private void Start()
    {
        _isOn = false;
    }

    public void SingplayerChoose()
    {
        if (_isOn)
        {
            _button.image.sprite = _spriteDeafult;
            _isOn = false;
            foreach (GameObject i in _elements)
            {
                i.SetActive(false);
            }
        }
        else
        {
            _button.image.sprite = _spriteOn;
            _isOn = true;
            foreach (GameObject i in _elements)
            {
                i.SetActive(true);
            }
        }
    }
}
