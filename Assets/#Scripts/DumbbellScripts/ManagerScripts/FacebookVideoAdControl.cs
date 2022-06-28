using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacebookVideoAdControl : MonoBehaviour
{
    [Header("===Ad Video Options===")]
    public bool includeEmoji = false;
    public bool cursorEnabled = false;
    public bool topBannerEnabled = false;
    public bool cursorEnabledOnClick = false;

    [Header("===Ad Video Variables")]
    public string[] adTexts;
    public Sprite emoji;
    
    Image emojiImg;
    GameObject topBanner;
    GameObject adHeaderEmoji;
    GameObject adHeaderText;
    GameObject cursorObj;
    GameObject emojiObj;
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();
    int adTextNumber = 0;
    
    void Awake()
    {
        Exceptions();
        InitializeTopBanner();
    }

    void Update()
    {
        CursorState();
        ChangeAdText();
        ChangeTextContainer();
    }

 #region Facebook Video    
    public void CursorState()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)) { cursorEnabled =! cursorEnabled; cursorObj.SetActive(cursorEnabled); }
        if(cursorObj.activeInHierarchy) cursorObj.transform.position = Input.mousePosition;
        if(Input.GetKeyDown(KeyCode.Keypad9)) cursorObj.transform.localScale += (Vector3.one * 0.2f);
        if(Input.GetKeyDown(KeyCode.Keypad6)) cursorObj.transform.localScale -= (Vector3.one * 0.2f);
        if(!cursorEnabledOnClick) return;
        if(Input.GetMouseButton(0)) cursorObj.SetActive(true);
        else cursorObj.SetActive(false);
    }
    public void ChangeAdText()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) adTextNumber--;
        if(Input.GetKeyDown(KeyCode.RightArrow)) adTextNumber++;
        
        if(adTextNumber > adTexts.Length) adTextNumber = adTexts.Length;
        else if(adTextNumber < 0) adTextNumber = 0;

        SetAdText();
    }

    public void InitializeTopBanner()
    {
        topBanner = transform.GetChild(1).gameObject;
        adHeaderText = topBanner.transform.GetChild(0).gameObject;
        adHeaderEmoji = topBanner.transform.GetChild(1).gameObject;
        cursorObj = transform.GetChild(0).gameObject;
        emojiObj = transform.GetChild(2).gameObject;

        if (adHeaderText != null) 
        {
            texts.Add(adHeaderText.GetComponent<TextMeshProUGUI>());
            adHeaderText.SetActive(!includeEmoji);
        }

        if (adHeaderEmoji != null) 
        {
            texts.Add(adHeaderEmoji.GetComponent<TextMeshProUGUI>());
            adHeaderEmoji.SetActive(includeEmoji);
            emojiImg = emojiObj.GetComponent<Image>();
            emojiObj.SetActive(includeEmoji);
        }

        if (topBanner != null) 
        {
            topBanner.SetActive(topBannerEnabled);
            if(topBannerEnabled) Debug.Log("Initialized Top Banner");
        }
        cursorObj.SetActive(cursorEnabled);
    }

    public void ChangeTextContainer()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            includeEmoji =! includeEmoji;
            adHeaderText.SetActive(!includeEmoji);
            adHeaderEmoji.SetActive(includeEmoji);
            emojiObj.SetActive(includeEmoji);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            topBannerEnabled =! topBannerEnabled;
            topBanner.SetActive(topBannerEnabled);
            emojiObj.SetActive(topBannerEnabled);
        }
        emojiImg.sprite = emoji;
    }

    public void Exceptions()
    {
        if(adTexts == null) { Debug.Log("Error : Please add some texts to Ad Texts container"); return; } 
        if(emoji == null) { Debug.Log ("Error : Please add your emoji to Emoji Image Container"); return; } 
    }

    public void SetAdText()
    {
        if(!topBannerEnabled || adTexts == null) return;
        foreach(TextMeshProUGUI txt in texts) txt.text = adTexts[adTextNumber];
    }
#endregion
}
