using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite[] _skins;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Image _choosedSkinImage;
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _advertismentButton;

    private Button _selectedSkinButton;
    private int _selectedSkin;
    private int _choosedSkin;
    private int[] _skinsAccess;
    private string[] _saveSkinNames;

    void Start()
    {
        _skinsAccess = new int[_skins.Length];
        _saveSkinNames = new string[_skins.Length];

        _skinsAccess[0] = PlayerPrefs.GetInt(_saveSkinNames[0], 1);
        for (int i = 1; i < _skins.Length; ++i)
        {
            _saveSkinNames[i] = _skins[i].name;
            _skinsAccess[i] = PlayerPrefs.GetInt(_saveSkinNames[i], 0);
            _skinsAccess[i] = 1;
        }

        _choosedSkin = PlayerPrefs.GetInt("Skin", 0);
        _selectedSkinButton = _buttons[_choosedSkin];
        _selectedSkinButton.interactable = false;
        _selectButton.interactable = false;
        _choosedSkinImage.sprite = _skins[_choosedSkin];
        _selectedSkin = _choosedSkin;
    }

    public void SkinChange()
    {
        PlayerPrefs.SetInt("Skin", _selectedSkin);
        _choosedSkinImage.sprite = _skins[_selectedSkin];
        _selectButton.interactable = false;
        _choosedSkin = _selectedSkin;
        PlayerPrefs.Save();
    }

    public void SkinSelect(int skin)
    {
        _buttons[skin].interactable = false;
        _selectedSkin = skin;

        _selectedSkinButton.interactable = true;
        _selectedSkinButton = _buttons[skin];

        if(_skinsAccess[skin] == 0)
        {
            _advertismentButton.gameObject.SetActive(true);
            _selectButton.gameObject.SetActive(false);
        }
        else
        {
            _advertismentButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }

        if (skin == _choosedSkin) _selectButton.interactable = false;
        else _selectButton.interactable = true;
    }

    public void LaunchAdvertisement()
    {
        //Launch Ad for _selectedSkin

        _skinsAccess[_selectedSkin]++;
        _advertismentButton.gameObject.SetActive(false);
        _selectButton.gameObject.SetActive(true);

        PlayerPrefs.SetInt(_saveSkinNames[_selectedSkin], _skinsAccess[_selectedSkin]);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
