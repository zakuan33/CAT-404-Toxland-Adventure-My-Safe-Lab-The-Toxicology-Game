using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MBLchoose : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image Rimage;
    public TMP_Text QText, RText;
    public Button NextButton, PreButton;
    public Toggle toggle1, toggle2;
    public Image checkmark1, checkmark2;
    private GameObject resultCanvas,correctCanvas;
    private GameObject chooseCanvas;

    //use List to store the random number
    public List<int> RandomNumber = new List<int>();
    public string[] imagePaths = { "MBL/1.png", "MBL/2.png", "MBL/3.png", "MBL/4.png", "MBL/5.png", "MBL/6.png", "MBL/7.png", "MBL/8.png", "MBL/9.png" };
    private Texture2D[] textures;
    public string[] explain =
    {
        "SIFAT BAHAN: Toksik\nKETERANGAN: Bahan kimia yang boleh menyebabkan kesan keracunan jika dihidu, ditelan atau diserap melalui kulit dalam jumlah yang sedikit dan masa yang singkat.\nCONTOH BAHAN: Sebatian arsenik, sianida, anilina, dan raksa",
        "SIFAT BAHAN: Berbahaya kepada persekitaran akuatik\nKETERANGAN: Boleh menyebabkan kesan mudarat kepada hidupan akuatik dan juga kesan buruk tergdap alam sekitar yang kekal berpanjangan.\nCONTOH BAHAN: Anilina, pentana, petroleum benzin, dan tetraklorometana",
        "SIFAT BAHAN: Merengsa\nKETERANGAN: Bahan ini secara umumnya tidak menghakis tetapi boleh merengsa(ruam, kesan kemerahan) pada kulit atau bahagian badan yang terdedah.\nCONTOH BAHAN: Alil isotiosianat, sikloheksil isosianat, dan etil isosianat",
        "SIFAT BAHAN: Mudah meletup/ meledak\nKETERANGAN: Bahan ini boleh meletup jika bergeser atau terdedah kepada haba/ tekanan yang tinggi.\nCONTOH BAHAN: Asid pikrik, benzoil peroksida, hidrazina nitrat, dan merkuri fulminat",
        "SIFAT BAHAN: Menghakis\nKETERANGAN: Jika terkena pada sebarang permukaan, bahan ini boleh menghakis permukaan tersebut termasuk kulit dan mata. Boleh menyebabkan kecederaan teruk terutamanya jika terkena dalam kuantiti yang banyak.\nCONTOH BAHAN: Asid kuat(HCI, HNO3) alkali, asetik anhidrida, dan benzoil klorida",
        "SIFAT BAHAN: Mudah terbakar\nKETERANGAN: Bahan yang mudah terbakar jika terdedah kepada sumber nyalaan, api atau haba yang tinggi.\nCONTOH BAHAN: Alkohol, butana, karbon monoksida, hidrgen, dan asetilina",
        "SIFAT BAHAN: Mengoksida/ oksidasi\nKETERANGAN: Bahan yang boleh menyebabkan kebakaran walaupun tanpa udara atau memarakkan api dalam bahan yang mudah terbakar.\nCONTOH BAHAN: Asid perklorik, hidrogen peroksida, barium dioksida, dan semua jenis nitrat",
        "SIFAT BAHAN: Bahaya kesihatan\nKETERANGAN: Bahan ini boleh menyebabkan kesan kepada kesihatan seperti kerosakan organ badan atau kanser jika terdedah sama ada dalam jangka masa pendek atau panjang.\nCONTOH BAHAN: Asid sulfurik, benzena, dan karbon tetraklorida",
        "SIFAT BAHAN: Gas di bawah tekanan\nKETERANGAN: Gas dalam bentuk termampat, tercair, tercair sejuk, dan terlarut. Gas yang dibebaskan sangat sejuk dan boleh menyebabkan kecederaan kriogenik. Balang gas boleh meletup jika dipanaskan.\nCONTOH BAHAN: Gas dalam silinder atau bekas aerosol"
    };
    //initial the number of mouse click
    private int clicks = 0;
    public float displayTime = 1.5f;
    int k,s;
    private bool showCorrect;

    // Start is called before the first frame update
    void Start()
    {
        //find the path
        resultCanvas = GameObject.Find("ChooseCanvas/FinishCanvas");
        correctCanvas = GameObject.Find("ChooseCanvas/CorrectCanvas");
        chooseCanvas = GameObject.Find("ChooseCanvas");
        image1 = chooseCanvas.transform.Find("Answer1Toggle/1Label").GetComponent<Image>();
        image2 = chooseCanvas.transform.Find("Answer2Toggle/2Label").GetComponent<Image>();
        Rimage = correctCanvas.transform.Find("CorrectImage").GetComponent<Image>();
        QText = chooseCanvas.transform.Find("QuestionImage/QText").GetComponent<TextMeshProUGUI>();
        RText= correctCanvas.transform.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        toggle1 = chooseCanvas.transform.Find("Answer1Toggle").GetComponent<Toggle>();
        toggle2 = chooseCanvas.transform.Find("Answer2Toggle").GetComponent<Toggle>();
        NextButton = chooseCanvas.transform.Find("ANextButton").GetComponent<Button>();
        PreButton = chooseCanvas.transform.Find("APreButton").GetComponent<Button>();
        checkmark1 = toggle1.graphic as Image;
        checkmark2 = toggle2.graphic as Image;


        resultCanvas.SetActive(false);
        correctCanvas.SetActive(false);
        //random number
        int r;
        while (RandomNumber.Count < 9)
        {
            r = UnityEngine.Random.Range(0, explain.Length);
            if (!RandomNumber.Contains(r))
            {
                RandomNumber.Add(r);
            }
            else
            {
                continue;
            }
        }

        textures = new Texture2D[imagePaths.Length];
        for (int i = 0; i < imagePaths.Length; i++)
        {
            string filePath = string.Format("{0}/{1}", Application.streamingAssetsPath, imagePaths[i]);

            // load from local
            byte[] bytes = File.ReadAllBytes(filePath);
            textures[i] = new Texture2D(2, 2); // initial
            textures[i].LoadImage(bytes);
        }

        if (clicks == 0)
        {
            PreButton.interactable = false;
        }

        k = RandomNumber[clicks];
        QText.text = explain[k];
        bool isAnswerOnLeft = Random.value < 0.5f;
        if (isAnswerOnLeft)
        {
            image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image2.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
        else
        {
            image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image1.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
        toggle1.onValueChanged.AddListener(OnToggleValueChanged1);
        toggle2.onValueChanged.AddListener(OnToggleValueChanged2);
        NextButton.onClick.AddListener(NextOnClick);
        PreButton.onClick.AddListener(PreOnClick);
    }

    private void Circle()
    {
        //++clicks;
        if (clicks == 8)
            resultCanvas.SetActive(true);
    }

    private void ResetToggle1()
    {
        toggle1.isOn = false;
        checkmark1.enabled = false;
    }

    private void ResetToggle2()
    {
        toggle2.isOn = false;
        checkmark2.enabled = false;
    }

    private IEnumerator ShowCorrect()
    {
        yield return new WaitForSeconds(displayTime);
        correctCanvas.SetActive(false);
        Rimage.enabled = false;
        RText.enabled = false;
    }

    private void OnToggleValueChanged1(bool value)
    {
        if (value)
        {
            checkmark1.enabled = true;
            Invoke("ResetToggle1", displayTime);
            if (image1.sprite.texture != textures[k])
            {
                correctCanvas.SetActive(true);
                Rimage.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                RText.text = explain[k];
                Rimage.enabled = true;
                RText.enabled = true;
                StartCoroutine(ShowCorrect());
                showCorrect = true;
            }
        }
    }

    private void OnToggleValueChanged2(bool value)
    {
        if (value)
        {
            checkmark2.enabled = true;
            Invoke("ResetToggle2", displayTime);
            if (image2.sprite.texture != textures[k])
            {
                correctCanvas.SetActive(true);
                Rimage.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                RText.text = explain[k];
                Rimage.enabled = true;
                RText.enabled = true;
                StartCoroutine(ShowCorrect());
                showCorrect = true;
            }
        }
    }


    void NextOnClick()
    {
        PreButton.interactable = true;
        clicks++;
        k = RandomNumber[clicks];
        QText.text = explain[k];
        bool isAnswerOnLeft = Random.value < 0.5f;
        if (isAnswerOnLeft)
        {
            image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image2.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
        else
        {
            image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image1.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
        Circle();
    }

    void PreOnClick()
    {
        clicks--;
        if (clicks == 0)
        {
            PreButton.interactable = false;
        }
        k = RandomNumber[clicks];
        QText.text = explain[k];
        bool isAnswerOnLeft = Random.value < 0.5f;
        if (isAnswerOnLeft)
        {
            image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image2.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
        else
        {
            image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
            do
            {
                s = UnityEngine.Random.Range(0, explain.Length);
            } while (s == k);
            image1.sprite = Sprite.Create(textures[s], new Rect(0, 0, textures[s].width, textures[s].height), Vector2.zero);
        }
    }
}