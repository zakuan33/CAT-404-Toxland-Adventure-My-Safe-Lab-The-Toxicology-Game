using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class GameCanvas : MonoBehaviour
{
    private GameObject matchCanvas;
    private GameObject situationCanvas;

    //Situation Canvas
    public TextMeshProUGUI SituationStoryText;
    public Image image;
    public Button ok;
    //Store the number of clicks
    private int clicks = 0;
    //use List to store the random number
    public List<int> RandomNumber = new List<int>();
    public string[] situation =
    {
        "'Aina ingin mengambil barangnya yang tertinggal di makmal. Ketika berada di makmal, Aina terasa pening kerana terhidu gas penunu Bunsen yang bocor'",
        "'Aina melakukan satu eksperimen di makmal dengan menggunakan alkali kuat. Tiba-tiba sedikit bahan kimia terpercik ke dalam meta Aina. Mata Aina terasa sangat pedih.'" ,
        "'Auni membawa botol air minuman ke dalam makmal. Ketika menjalankan satu eksperimen, Auni berasa dahaga dan membuka boyol air mimuman untuk diminum namum terhenti kerana diusik Aina yang mengacah untuk menumpahkan bahan kimia ke atas Auni. Sedikit bahan kimia termasuk ke dalam air minuman tanpa disedari dan Auni meminumnya.'",
        "'Semasa mengendalikan Hidrogen Peroksida di makmal, tiba-tiba tangan Auni terkena cecair Hidrogen Peroksida tersebut kerana Auni tidak memakai sarung tangan. Tangan Auni kemerah-merahan dan terasa pedih.'",
        "'Auni dan Adam bermain dengan botol penyembur bahan pencuci yang terdapat di dalam makmal. Tiba-tiba Adam tersembur bahan pencuci tersebut ke arah mata Auni. Mata Auni terasa amat pedih.'",
        "'Adam sedang menjalankan satu eksperimen di makmal tanpa pengetahun cikgunya. Adam mencampurkan 2 bahan kimia yang terdapat di dalam makmal lalu menghasilkan gas klorin yang berwarna hijau dan beracun. Adam terhidu gas tersebut.'",
        "'Ketika sedang belajar mengenai bahan kimia di makmal sekolah, Auni dan Aina bermain-main dengan satu bahan kimia yang mempunyai warna yang sangat menarik. Bahan kimia itu terpercik masuk ke dalam mulut Auni.'",
        "'Ketika ingin mengambil bahan kimia di makmal, tangan Adam tertolak satu bikar yang mengandungi asid kuat jatuh dari meja. Asid itu terkena kaki Adam.'",
        "'Auni sedang mengandalikan serbuk bahan kimia dalam satu eksperimen. Adam mengacah bahan kimia tersebut. Sedikit serbuk bahan kimia termasuk ke dalam mulut Auni'",
        "'Ketika Adam sedang menjalankan satu eksperimen, sedikit bahan kimia terpercik masuk ke dalam mata Adam. Mata Adam kemerah-merahan dan berasa pedih.'",
        "'Ketika Adam membuka botol bahan kimia yang berisi larutan Ammonia di luar kebuk wasap, Adam terhidu sedikit gas Ammonia yang terbebas. Adam berasa sesak nafas dan terbatuk-batuk.'",
        "'Selepas menjalankan suatu eksperimen, Aina ingin mencuci bikar yang digunakan untuk mengisi Natrium Hidrokside yang pekat. Ketika mencuci, sedikit bahan kimia itu terkena tangan Aina.'"
    };

    //place the relative image path
    public string[] imagePaths = { "ImHero/07.png", "ImHero/04.png", "ImHero/08.png", "ImHero/03.png", "ImHero/04.png", "ImHero/07.png", "ImHero/08.png", "ImHero/03.png", "ImHero/08.png", "ImHero/04.png", "ImHero/07.png", "ImHero/03.png" };
    private Texture2D[] textures;

    public Image Qimage, image1, image2;
    public Image SuccessImage, FailImage;
    public Image checkmark1, checkmark2;
    //To store the correct answer image
    public Image CorrectI;
    public Toggle toggle1, toggle2;
    private GameObject ResultCanvas, FinishCanvas;

    public string[] Paths1 = { "ImHero/01.png", "ImHero/06.png", "ImHero/02.png", "ImHero/05.png", "ImHero/06.png", "ImHero/01.png", "ImHero/02.png", "ImHero/05.png", "ImHero/02.png", "ImHero/06.png", "ImHero/01.png", "ImHero/05.png" };
    public string[] Rpaths = { "ImHero/effect_correct.png", "ImHero/effect_boom.png" };
    private Texture2D[] Qtextures, textures1, textures2, Rtexture;
    //the time of showing the result image
    public float displayTime = 1.5f;
    /*//To account the time
    private float timeLeft;*/
    //Whether to show the image
    private bool showSuccessImage, showFailImage;

    //initial the number of mouse click
    private int k;


    // Start is called before the first frame update
    void Start()
    {
        matchCanvas = GameObject.Find("Canvas/MatchCanvas");
        situationCanvas = GameObject.Find("Canvas/SituationCanvas");

        //Situation Canvas
        SituationStoryText = situationCanvas.transform.Find("SituationStoryText").GetComponent<TextMeshProUGUI>();
        image = situationCanvas.transform.Find("CorrectImage").GetComponent<Image>();
        ok = situationCanvas.transform.Find("OkButton").GetComponent<Button>();

        //Answer Label Canvas
        ResultCanvas = GameObject.Find("Canvas/MatchCanvas/ResultCanvas");
        FinishCanvas = GameObject.Find("Canvas/MatchCanvas/FinishCanvas");

        Qimage = matchCanvas.transform.Find("SituationImage").GetComponent<Image>();
        image1 = matchCanvas.transform.Find("Answer1Toggle/Label1").GetComponent<Image>();
        image2 = matchCanvas.transform.Find("Answer2Toggle/Label2").GetComponent<Image>();

        SuccessImage = ResultCanvas.transform.Find("SuccessImage").GetComponent<Image>();
        FailImage = ResultCanvas.transform.Find("FailImage").GetComponent<Image>();
        toggle1 = matchCanvas.transform.Find("Answer1Toggle").GetComponent<Toggle>();
        toggle2 = matchCanvas.transform.Find("Answer2Toggle").GetComponent<Toggle>();

        checkmark1 = toggle1.graphic as Image;
        checkmark2 = toggle2.graphic as Image;

        int r;
        while (RandomNumber.Count < 12)
        {
            r = UnityEngine.Random.Range(0, situation.Length);
            if (!RandomNumber.Contains(r))
            {
                RandomNumber.Add(r);
            }
            else
            {
                continue;
            }
        }

        //image
        textures = new Texture2D[imagePaths.Length];
        for (int i = 0; i < imagePaths.Length; i++)
        {
            string filePath = string.Format("{0}/{1}", Application.streamingAssetsPath, imagePaths[i]);
            // load from local
            byte[] bytes = File.ReadAllBytes(filePath);
            //initial
            textures[i] = new Texture2D(2, 2);
            textures[i].LoadImage(bytes);
        }
        
        Qtextures = textures;
        textures1 = new Texture2D[Paths1.Length];
        for (int i = 0; i < textures1.Length; i++)
        {
            string Path1 = string.Format("{0}/{1}", Application.streamingAssetsPath, Paths1[i]);
            // load from local
            byte[] bytes1 = File.ReadAllBytes(Path1);
            textures1[i] = new Texture2D(2, 2); // initial
            textures1[i].LoadImage(bytes1);
        }

        textures2 = textures1;
  

        Rtexture = new Texture2D[Rpaths.Length];
        for (int i = 0; i < Rtexture.Length; i++)
        {
            string Rpath = string.Format("{0}/{1}", Application.streamingAssetsPath, Rpaths[i]);
            // load from local
            byte[] Rbyte = File.ReadAllBytes(Rpath);
            Rtexture[i] = new Texture2D(2, 2); // initial
            Rtexture[i].LoadImage(Rbyte);
        }

        SuccessImage.sprite = Sprite.Create(Rtexture[0], new Rect(0, 0, Rtexture[0].width, Rtexture[0].height), Vector2.zero);
        FailImage.sprite = Sprite.Create(Rtexture[1], new Rect(0, 0, Rtexture[1].width, Rtexture[1].height), Vector2.zero);

        matchCanvas.SetActive(false);
        situationCanvas.SetActive(true);
        //Given the SituationText and image
        int k = RandomNumber[clicks];
        SituationStoryText.text = situation[k];
        image.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
        ok.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
    }

    // Update is called once per frame
    void Update()
    {
        k = RandomNumber[clicks];
        SituationStoryText.text = situation[k];
        image.sprite = Sprite.Create(textures[k], new Rect(0, 0, textures[k].width, textures[k].height), Vector2.zero);
        ok.onClick.AddListener(OnClick);
        //Ensure the mouse click the OK button

        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(ok.GetComponent<RectTransform>(), Input.mousePosition))
            {
                situationCanvas.SetActive(false);
                matchCanvas.SetActive(true);

                ResultCanvas.SetActive(false);
                FinishCanvas.SetActive(false);
                //k = RandomNumber[clicks];
                int l;
                do
                {
                    l = UnityEngine.Random.Range(0, imagePaths.Length);
                } while (textures1[l] == textures1[k]);

                Qimage.sprite = Sprite.Create(Qtextures[k], new Rect(0, 0, Qtextures[k].width, Qtextures[k].height), Vector2.zero);
                image1.sprite = Sprite.Create(textures1[k], new Rect(0, 0, textures1[k].width, textures1[k].height), Vector2.zero);
                image2.sprite = Sprite.Create(textures2[l], new Rect(0, 0, textures2[l].width, textures2[l].height), Vector2.zero);

                toggle1.onValueChanged.AddListener(OnToggleValueChanged1);
                toggle2.onValueChanged.AddListener(OnToggleValueChanged2);
                //clicks++;
            }
        }
    }

    private void Circle()
    {
        //++clicks;
        if (clicks < 12)
        {
            situationCanvas.SetActive(true);
            matchCanvas.SetActive(false);
        }
        else if(clicks >= 12)
            JumpToFinish();
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

    private IEnumerator ShowSuccessImage()
    {
        yield return new WaitForSeconds(displayTime);
        ResultCanvas.SetActive(false);
        SuccessImage.enabled = false;
        Circle();
    }

    private IEnumerator ShowFailImage()
    {
        yield return new WaitForSeconds(displayTime);
        ResultCanvas.SetActive(false);
        FailImage.enabled = false;
        Circle();
    }

    private void OnToggleValueChanged1(bool value)
    {
        if (value)
        {
            checkmark1.enabled = true;
            Invoke("ResetToggle1", displayTime);

            if (image1.sprite.texture == textures1[k])
            {
                //ShowSuccessImage();
                ResultCanvas.SetActive(true);
                FailImage.enabled = false;
                SuccessImage.enabled = true;
                StartCoroutine(ShowSuccessImage());
                showSuccessImage = true;
            }
            // Jump to the next canvas
            else if(image1.sprite.texture != textures1[k])
            {
                ResultCanvas.SetActive(true);
                SuccessImage.enabled = false;
                FailImage.enabled = true;
                StartCoroutine(ShowFailImage());
                showFailImage = true;
            }
        }
    }

    private void OnToggleValueChanged2(bool value)
    {
        if (value)
        {
            checkmark2.enabled = true;
            Invoke("ResetToggle2", displayTime);
            if (image2.sprite.texture == textures1[k])
            {
                //ShowSuccessImage();
                ResultCanvas.SetActive(true);
                FailImage.enabled = false;
                SuccessImage.enabled = true;
                StartCoroutine(ShowSuccessImage());
                showSuccessImage = true;
            }
            // Jump to the next canvas
            else if (image2.sprite.texture != textures1[k])
            {
                ResultCanvas.SetActive(true);
                SuccessImage.enabled = false;
                FailImage.enabled = true;
                StartCoroutine(ShowFailImage());
                showFailImage = true;
            }
        }
    }

    private void JumpToFinish()
    {
        // Enable the next canvas
        FinishCanvas.SetActive(true);
    }
}
