using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeController : MonoBehaviour
{
    public ColorTheme currentColor;
    public ColorTheme lightColor;
    public ColorTheme darkColor;

    public GameObject panelInfo;

    private void ChangeThemeForAll()
    {
        Image imageBackgrownd = GameObject.Find("PanelBackgrownd").GetComponent<RectTransform>().GetComponent<Image>();
        imageBackgrownd.sprite = currentColor.backgroundPanel;


        Image imageEasy = GameObject.Find("ButtonEasy").GetComponent<Button>().GetComponent<Image>();
        imageEasy.sprite = currentColor.easyButton;

        Image imageStandart = GameObject.Find("ButtonStandart").GetComponent<Button>().GetComponent<Image>();
        imageStandart.sprite = currentColor.standartButton;

        Image imageHard = GameObject.Find("ButtonHard").GetComponent<Button>().GetComponent<Image>();
        imageHard.sprite = currentColor.hardButton;

        Image imageRectangle = GameObject.Find("ButtonRectangle").GetComponent<Button>().GetComponent<Image>();
        imageRectangle.sprite = currentColor.buttomRectangle;

        Image imageTriangle = GameObject.Find("ButtonTriangle").GetComponent<Button>().GetComponent<Image>();
        imageTriangle.sprite = currentColor.buttomTriangle;

        Image imageHexagonal = GameObject.Find("ButtonHexagon").GetComponent<Button>().GetComponent<Image>();
        imageHexagonal.sprite = currentColor.buttomHexagonal;

        Image imageSun = GameObject.Find("ButtonSun").GetComponent<Button>().GetComponent<Image>();
        imageSun.sprite = currentColor.sunButton;


        Image imageSize = GameObject.Find("PanelSize").GetComponent<RectTransform>().GetComponent<Image>();
        imageSize.sprite = currentColor.panelSize;

        Image imageInfo = GameObject.Find("ButtonInfo").GetComponent<Button>().GetComponent<Image>();
        imageInfo.sprite = currentColor.infoBotton;


        Image imageMusic = GameObject.Find("ButtonMusic").GetComponent<Button>().GetComponent<Image>();
        imageMusic.sprite = currentColor.musicBotton;

        Image imageLeft = GameObject.Find("ButtonLeft").GetComponent<Button>().GetComponent<Image>();
        imageLeft.sprite = currentColor.leftButton;

        Image imageRight = GameObject.Find("ButtonRight").GetComponent<Button>().GetComponent<Image>();
        imageRight.sprite = currentColor.rightButton;

        Image imageRightSky = GameObject.Find("ImageRightSky").GetComponent<Image>();
        imageRightSky.sprite = currentColor.rightSky;

        TextMeshProUGUI mineCount = GameObject.Find("TextCountMines").GetComponent<TextMeshProUGUI>();
        mineCount.color = new Color(currentColor.r_sky / 255f, currentColor.g_sky / 255f, currentColor.b_sky / 255f);

        TextMeshProUGUI timertext = GameObject.Find("TextTimer").GetComponent<TextMeshProUGUI>();
        timertext.color = new Color(currentColor.r_sky / 255f, currentColor.g_sky / 255f, currentColor.b_sky / 255f);

        TextMeshProUGUI wtext = GameObject.Find("TextWidht").GetComponent<TextMeshProUGUI>();
        wtext.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI htext = GameObject.Find("TextHeight").GetComponent<TextMeshProUGUI>();
        htext.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI contminetext = GameObject.Find("TextCountMinesSlider").GetComponent<TextMeshProUGUI>();
        contminetext.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI wtext_num = GameObject.Find("TextWidthNum").GetComponent<TextMeshProUGUI>();
        wtext_num.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI htext_num = GameObject.Find("TextHeightNum").GetComponent<TextMeshProUGUI>();
        htext_num.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI cmtext_num = GameObject.Find("TextCountMinesNum").GetComponent<TextMeshProUGUI>();
        cmtext_num.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        Image imageLeftSky = GameObject.Find("ImageLeftSky").GetComponent<Image>();
        imageLeftSky.sprite = currentColor.leftSky;

        TextMeshProUGUI question = GameObject.Find("TextButtonQuestion").GetComponent<TextMeshProUGUI>();
        question.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        Image imageQuestion = GameObject.Find("ButtonQuestion").GetComponent<Button>().GetComponent<Image>();
        imageQuestion.sprite = currentColor.questionButton;

        Image imageExit = GameObject.Find("ButtonExit").GetComponent<Button>().GetComponent<Image>();
        imageExit.sprite = currentColor.exitButton;

        TextMeshProUGUI textExit = GameObject.Find("TextExit").GetComponent<TextMeshProUGUI>();
        textExit.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);


        panelInfo.SetActive(true);
        Image imageInfoPanel = panelInfo.GetComponent<Image>();
        imageInfoPanel.sprite = currentColor.infoPanel;

        TextMeshProUGUI textRules = GameObject.Find("TextRules").GetComponent<TextMeshProUGUI>();
        textRules.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI textBestTime = GameObject.Find("TextBestTime").GetComponent<TextMeshProUGUI>();
        textBestTime.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI textBestTimeText = GameObject.Find("TextBestTimeText").GetComponent<TextMeshProUGUI>();
        textBestTimeText.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI textBestTimeEasy = GameObject.Find("TextEasyTimeValue").GetComponent<TextMeshProUGUI>();
        textBestTimeEasy.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI textBestTimeStandart = GameObject.Find("TextStandartTimeValue").GetComponent<TextMeshProUGUI>();
        textBestTimeStandart.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

        TextMeshProUGUI textBestTimeHard = GameObject.Find("TextHardTimeValue").GetComponent<TextMeshProUGUI>();
        textBestTimeHard.color = new Color(currentColor.r / 255f, currentColor.g / 255f, currentColor.b / 255f);

       // Image imageInfoOk = GameObject.Find("ButtonInfoOk").GetComponent<Button>().GetComponent<Image>();
        //imageInfoOk.sprite = currentColor.infoOkButton;

        panelInfo.SetActive(false);
    }

    public void ChangeColorTheme()
    {
        if (currentColor == lightColor)
        {
            currentColor = darkColor;
        }
        else
        {
            currentColor = lightColor;
        }
        ChangeThemeForAll();
    }
}
