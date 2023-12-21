using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPopupController : MonoBehaviour
{
    [HideInInspector] public string profileName;
    public TMP_InputField inputField;

    public SaveSlot activeSaveSlot;

    public void ReadStringInput(string s, SaveSlot saveSlot)
    {

    }

    public void DeactivatePopup()
    {
        this.gameObject.SetActive(false);
    }
}
