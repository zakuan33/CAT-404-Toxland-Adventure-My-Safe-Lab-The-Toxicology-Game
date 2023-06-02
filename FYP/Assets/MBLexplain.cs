using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MBLexplain : MonoBehaviour
{
    public Image image1;
    public TextMeshProUGUI text1;

    public Image image2;
    public TextMeshProUGUI text2;

    public Button NextButton, PreButton;

    private GameObject canvasGameObject;
    private GameObject chooseCanvas;
    //use List to store the random number
    private List<int> RandomNumber = new List<int>();
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
    private int clicks;

    // Start is called before the first frame update
    void Start()
    {
        //find the path
        canvasGameObject = GameObject.Find("GameCanvas");
        chooseCanvas = GameObject.Find("ChooseCanvas");
        chooseCanvas.SetActive(false);

        image1 = canvasGameObject.transform.Find("Show1Toggle/Background1").GetComponent<Image>();
        text1 = canvasGameObject.transform.Find("Show1Toggle/Text1").GetComponent<TextMeshProUGUI>();
        image2 = canvasGameObject.transform.Find("Show2Toggle/Background2").GetComponent<Image>();
        text2 = canvasGameObject.transform.Find("Show2Toggle/Text2").GetComponent<TextMeshProUGUI>();

        NextButton = canvasGameObject.transform.Find("NextButton").GetComponent<Button>();
        PreButton = canvasGameObject.transform.Find("PreButton").GetComponent<Button>();
        PreButton.interactable = false;

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
        clicks = 0;
        int k = RandomNumber[clicks];
        image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
        text1.text = explain[k];

        ++clicks;
        k = RandomNumber[clicks];
        image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
        text2.text = explain[k];

        NextButton.onClick.AddListener(NextOnClick);
        PreButton.onClick.AddListener(PreOnClick);
        
    }

    //After mouse click
    void Update()
    {
        // check whether click the mouse
        if (Input.GetMouseButtonDown(0))
        {
            // ensure the mourse in the position of NextButton
            if (RectTransformUtility.RectangleContainsScreenPoint(NextButton.GetComponent<RectTransform>(), Input.mousePosition))
            {
                // clicks+1
                int k = RandomNumber[clicks];
                image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                text1.text = explain[k];

                clicks++;
                k = RandomNumber[clicks];
                image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                text2.text = explain[k];
                //NextButton.onClick.AddListener(NextOnClick);
            }

            if (RectTransformUtility.RectangleContainsScreenPoint(PreButton.GetComponent<RectTransform>(), Input.mousePosition))
            {
                // clicks-1
                int k = RandomNumber[clicks];
                image2.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                text2.text = explain[k];

                clicks--;
                k = RandomNumber[clicks];
                image1.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
                text1.text = explain[k];

               // PreButton.onClick.AddListener(PreOnClick);
            }

            if (clicks == 8)
            {
                canvasGameObject.SetActive(false);
                chooseCanvas.SetActive(true);
            }

        }

    }

    //Change the number of click within user click
    void NextOnClick()
    {
        ++clicks;
        PreButton.interactable = true;
    }

    void PreOnClick()
    {
        --clicks;
        if (clicks == 1)
        {
            PreButton.interactable = false;
        }
    }
}
