using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeButtons : MonoBehaviour
{
    [Header("Static Colored Objects")]
    public Sprite[] coloredButtons; // 0 = blue , 1 = yellow , 2 = green , 3 = purple
    public Sprite[] coloredBackgrounds;

    [Header("All Image Objects in Scene")]
    public List<Image>images = new List<Image>();

    [Header("Hue")]
    public Color32 hue = Color.white;

    public void GetAllBG()
    {
        images = transform.parent.GetComponentsInChildren<Image>(true).ToList();
        print("All Images are listed");
    }

    public void ApplyHue()
    {
        foreach(Image i in images) i.color = hue;
        print("Hue changed");
    }

    public void ChangeColor(int x)
    {
        foreach(Image i in images)
        {
            if(i.sprite == null){print("No Image Found " + i.name);}
            else
            {
                if(i.sprite.name == "Btn_Blue") i.sprite = coloredButtons[x];
                if(i.sprite.name == "Btn_Yellow") i.sprite = coloredButtons[x];
                if(i.sprite.name == "Btn_Green") i.sprite = coloredButtons[x];
                if(i.sprite.name == "Btn_Purple") i.sprite = coloredButtons[x];
                
                if(i.sprite.name == "bg_blue") i.sprite = coloredBackgrounds[x];
                if(i.sprite.name == "bg_yellow") i.sprite = coloredBackgrounds[x];
                if(i.sprite.name == "bg_green") i.sprite = coloredBackgrounds[x];
                if(i.sprite.name == "bg_purple") i.sprite = coloredBackgrounds[x];
            }
        }
        print("Operation Complete");
    }
}
