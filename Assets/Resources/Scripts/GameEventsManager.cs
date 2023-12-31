using System.Collections.Generic;
using System.Linq;
using cowsins;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventsManager : MonoBehaviour, IDataPersistence
{
    public static GameEventsManager instance { get; private set; }

    private WeaponController _controller;
    private PlayerMovement _playerMovement;
    private PlayerStats _playerStats;
    private UIController _uiController;
    private CameraEffects _camera;
    private ExperienceManager _experienceManager;
    private CoinManager _coinManager;

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
        SceneManager.activeSceneChanged += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene oldScene, Scene newScene)
    {
        _controller = FindObjectOfType<WeaponController>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerStats = FindObjectOfType<PlayerStats>();
        _uiController = FindObjectOfType<UIController>();
        _camera = FindObjectOfType<CameraEffects>();
        _experienceManager = FindObjectOfType<ExperienceManager>();
        _coinManager = FindObjectOfType<CoinManager>();
    }

    public void LoadData(GameData gameData)
    {
        _playerMovement.transform.position = gameData.playerPosition;
        _playerStats.health = gameData.playerHealth;
        _playerStats.shield = gameData.playerShield;
        _playerStats.maxHealth = gameData.maxHealth;
        _playerStats.maxShield = gameData.maxShield;
        _playerStats.damageMultiplier = gameData.damageMultiplier;
        _playerStats.healMultiplier = gameData.healMultiplier;
        _camera.transform.rotation = gameData.playerCameraRotation;
        _camera.GetComponentInParent<MoveCamera>().transform.rotation = gameData.playerRotation;
        _experienceManager.playerLevel = gameData.playerLvl;
        _coinManager.AddCoins(gameData.coins);
        
        // Weapon Things
        /*_controller.inventory[0] = gameData.weaponOne.weaponObject;
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
        _controller.currentWeapon = gameData.currentWeaponInt;*/

        foreach (var weapon in gameData.weapons)
        {
            _controller.inventory[gameData.weapons.IndexOf(weapon)] = weapon.weaponObject;

            var weaponObject = Instantiate(weapon.weaponObject, _controller.weaponHolder);
            weaponObject.transform.localPosition = weapon.weaponObject.transform.localPosition;

            _controller.inventory[gameData.weapons.IndexOf(weapon)] = weaponObject;
            _controller.weapon = weaponObject.weapon;

            _controller.slots[gameData.weapons.IndexOf(weapon)].weapon = weaponObject.weapon;
            _controller.slots[gameData.weapons.IndexOf(weapon)].GetImage();
            
            foreach (var ammo in gameData.ammoCount)
            {
                _controller.inventory[gameData.weapons.IndexOf(weapon)].bulletsLeftInMagazine = ammo;
            }
        }

        _controller.currentWeapon = gameData.currentWeaponInt;
        _controller.SelectWeapon();
        
        // Update UI
        _uiController.UpdateHealthUI(gameData.playerHealth, gameData.playerShield, true);
        _uiController.UpdateCoins(gameData.coins);
        _uiController.UpdateXP();
    }

    public void SaveData(GameData gameData)
    {
        gameData.currentScene = SceneManager.GetActiveScene().name;
        
        gameData.playerPosition = _playerMovement.transform.position;
        gameData.playerHealth = _playerStats.health;
        gameData.playerShield = _playerStats.shield;
        gameData.maxHealth = _playerStats.maxHealth;
        gameData.maxShield = _playerStats.maxShield;
        gameData.damageMultiplier = _playerStats.damageMultiplier;
        gameData.healMultiplier = _playerStats.healMultiplier;
        gameData.playerCameraRotation = _camera.transform.rotation;
        gameData.playerRotation = _camera.GetComponentInParent<MoveCamera>().transform.rotation;
        gameData.playerLvl = _experienceManager.playerLevel + 1;
        gameData.coins = _coinManager.coins;

        foreach (var weapon in _controller.inventory)
        {
            if (gameData.weapons.Count > _controller.inventory.Length - 1)
            {
                gameData.weapons.Clear();
                if (gameData.weapons.Count <= 1)
                {
                    gameData.weapons.Add(weapon.weapon);
                }
            }
            else
            {
                gameData.weapons.Add(weapon.weapon);
            }

            if (gameData.ammoCount.Count > _controller.inventory.Length - 1)
            {
                gameData.ammoCount.Clear();
                if (gameData.ammoCount.Count <= 1)
                {
                    gameData.ammoCount.Add(weapon.bulletsLeftInMagazine);
                }
            }
            else
            {
                gameData.ammoCount.Add(weapon.bulletsLeftInMagazine);
            }
        }

        gameData.currentWeaponInt = _controller.currentWeapon;
        gameData.timeSaved = System.DateTime.Now.ToString();
    }
}
