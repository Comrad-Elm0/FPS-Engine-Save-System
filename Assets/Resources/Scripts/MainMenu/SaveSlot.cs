using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] string profileId = "";

    [Header("Content")]
    [SerializeField] GameObject noDataContent;
    [SerializeField] GameObject hasDataContent;
    [SerializeField] TextMeshProUGUI saveName;

    [Header("Clear Data Button")]
    [SerializeField] Button clearData;

    Button saveSlotButton;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearData.gameObject.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearData.gameObject.SetActive(true);
            saveName.text = data.currentScene;
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        clearData.interactable = interactable;
    }
}
