using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // Tewxt fields
    public Text levelText, hitPointText, doloradosText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right) {
        if (right) {
            currentCharacterSelection++;

            // If we went too far away
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count) {
                currentCharacterSelection = 0;
            }
            OnSelectionChanged();
        } else {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }
            OnSelectionChanged();
        }
    }
    
    private void OnSelectionChanged() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon upgrade
    public void OnUpgradeClick() {
        if(GameManager.instance.TryUpgradeWeapon()) {
            UpdateMenu();
        }
    }

    //Update the character information
    public void UpdateMenu() {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) {
            upgradeCostText.text = "Max";
        } else {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        // Meta
        hitPointText.text = GameManager.instance.player.hitpoint.ToString();
        doloradosText.text = GameManager.instance.dolorado.ToString();
        levelText.text = "Not Implemented!";

        // xp bar
        xpText.text = "Not Implemented";
        xpBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
