using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        if (GameManager.instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;

    public FloatingTextManager floatingTextManager;

    //Logic
    public int dolorado;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float dur) {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, dur);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon() {
        // is wapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel) {
            return false;
        }
        if (dolorado >= weaponPrices[weapon.weaponLevel]) {
            dolorado -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }


    public void SaveState() {
        string s = "";

        s += "0" + "|";
        s += dolorado.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode) {

        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        dolorado = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[3]));
        

        Debug.Log("LoadState");
    }
}
