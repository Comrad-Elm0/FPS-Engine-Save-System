using System;
using cowsins;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour, IDataPersistence
{
    public static GameEventsManager instance { get; private set; }

    private WeaponController _controller;
    private PlayerMovement _playerMovement;
    private CameraEffects _camera;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _controller = FindObjectOfType<WeaponController>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _camera = FindObjectOfType<CameraEffects>();
    }

    public void LoadData(GameData gameData)
    {
        _playerMovement.transform.position = gameData.playerPosition;
        _camera.transform.rotation = gameData.playerCameraRotation;
        _camera.GetComponentInParent<MoveCamera>().transform.rotation = gameData.playerRotation;
        
        _controller.inventory[0] = gameData.weaponOne.weaponObject;
        _controller.inventory[1] = gameData.weaponTwo.weaponObject;

        var wepOne = Instantiate(gameData.weaponOne.weaponObject, _controller.weaponHolder);
        wepOne.transform.localPosition = gameData.weaponOne.weaponObject.transform.localPosition;

        var wepTwo = Instantiate(gameData.weaponTwo.weaponObject, _controller.weaponHolder);
        wepTwo.transform.localPosition = gameData.weaponTwo.weaponObject.transform.localPosition;

        _controller.inventory[0] = wepOne;
        _controller.inventory[1] = wepTwo;
        _controller.weapon = wepOne.weapon;

        _controller.slots[0].weapon = wepOne.weapon;
        _controller.slots[0].GetImage();

        _controller.slots[1].weapon = wepTwo.weapon;
        _controller.slots[1].GetImage();

        _controller.inventory[0].bulletsLeftInMagazine = gameData.primaryWeaponAmmoCount;
        _controller.inventory[1].bulletsLeftInMagazine = gameData.secondaryWeaponAmmoCount;
        _controller.currentWeapon = gameData.currentWeaponInt;

        _controller.SelectWeapon();
    }

    public void SaveData(GameData gameData)
    {
        gameData.currentScene = SceneManager.GetActiveScene().name;
        
        gameData.playerPosition = _playerMovement.transform.position;
        gameData.playerCameraRotation = _camera.transform.rotation;
        gameData.playerRotation = _camera.GetComponentInParent<MoveCamera>().transform.rotation;
        
        gameData.primaryWeaponAmmoCount = _controller.inventory[0].bulletsLeftInMagazine;
        gameData.secondaryWeaponAmmoCount = _controller.inventory[1].bulletsLeftInMagazine;

        gameData.weaponOne = _controller.inventory[0].weapon;
        gameData.weaponTwo = _controller.inventory[1].weapon;
        
        gameData.currentWeaponInt = _controller.currentWeapon;

    }
}
