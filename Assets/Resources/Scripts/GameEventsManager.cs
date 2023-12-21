using cowsins;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour, IDataPersistence
{
    public static GameEventsManager instance { get; private set; }

    private WeaponController controller;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        controller = FindObjectOfType<WeaponController>();
    }

    public void LoadData(GameData gameData)
    {
        controller.inventory[0] = gameData.weaponOne.weaponObject;
        controller.inventory[1] = gameData.weaponTwo.weaponObject;

        var wepOne = Instantiate(gameData.weaponOne.weaponObject, controller.weaponHolder);
        wepOne.transform.localPosition = gameData.weaponOne.weaponObject.transform.localPosition;

        var wepTwo = Instantiate(gameData.weaponTwo.weaponObject, controller.weaponHolder);
        wepTwo.transform.localPosition = gameData.weaponTwo.weaponObject.transform.localPosition;

        controller.inventory[0] = wepOne;
        controller.inventory[1] = wepTwo;
        controller.weapon = wepOne.weapon;

        controller.slots[0].weapon = wepOne.weapon;
        controller.slots[0].GetImage();

        controller.slots[1].weapon = wepTwo.weapon;
        controller.slots[1].GetImage();

        controller.inventory[0].bulletsLeftInMagazine = gameData.primaryWeaponAmmoCount;
        controller.inventory[1].bulletsLeftInMagazine = gameData.secondaryWeaponAmmoCount;
        controller.currentWeapon = gameData.currentWeaponInt;

        controller.SelectWeapon();
    }

    public void SaveData(GameData gameData)
    {
        gameData.currentScene = SceneManager.GetActiveScene().name;

        gameData.weaponOne = controller.inventory[0].weapon;
        gameData.weaponTwo = controller.inventory[1].weapon;
        
        gameData.primaryWeaponAmmoCount = controller.inventory[0].bulletsLeftInMagazine;
        gameData.secondaryWeaponAmmoCount = controller.inventory[1].bulletsLeftInMagazine;
        gameData.currentWeaponInt = controller.currentWeapon;

    }
}
