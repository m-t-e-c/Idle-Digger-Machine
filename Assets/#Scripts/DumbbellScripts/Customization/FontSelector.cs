using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[ExecuteInEditMode]
public class FontSelector : MonoBehaviour
{
    [Header("All text Objects in Scene")]
    public List<TextMeshProUGUI>texts = new List<TextMeshProUGUI>();
    [Header("Currently applied font")]
    public TMP_Asset appliedFont;
    [Header("Change Font")]
    public TMP_FontAsset selectedFont;
    
    bool textFound = false;
    
    public void ChangeFont()
    {
        foreach(TextMeshProUGUI t in texts)
        {
            t.font = selectedFont;
        }
        print("Font changed to " + selectedFont.name);
    }
    public TMP_FontAsset CurrentFont()
    {
        TMP_FontAsset cf = texts[0].font;
        textFound = true;
        appliedFont = cf;
        print("Current font is " + cf);
        return cf;
    }
    public void GetTexts()
    {
        texts = transform.parent.GetComponentsInChildren<TextMeshProUGUI>(true).ToList();
        print("All texts are listed");
    }
    void Awake() 
    {
        GetTexts();    
    }
}

