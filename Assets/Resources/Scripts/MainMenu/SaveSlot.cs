using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")] 
    [SerializeField] private string profileId = "";

    [Header("Content")] 
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI saveName;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI lastPlayedText;


    [Header("Clear Data Button")] 
    [SerializeField] private Button clearData;

    private Button saveSlotButton;

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
            playerLevelText.text = $"LEVEL: {data.playerLvl.ToString()}";
            coinsText.text = $"COINS: {data.coins.ToString()}";
            lastPlayedText.text = $"LAST SAVED: {data.timeSaved}";
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
