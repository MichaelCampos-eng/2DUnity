using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public void Update() {
        foreach(FloatingText txt in floatingTexts) {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float dur) {
        FloatingText floatTxt = GetFloatingText(); 
        floatTxt.txt.text = msg;
        floatTxt.txt.fontSize = fontSize;
        floatTxt.txt.color = color;

        floatTxt.go.transform.position = Camera.main.WorldToScreenPoint(pos); //Transform world sapce to screen soace sow e can use it in UI
        floatTxt.motion = motion;
        floatTxt.duration = dur;

        floatTxt.Show();

    }

    private FloatingText GetFloatingText() {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        if (txt == null) {
            
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
