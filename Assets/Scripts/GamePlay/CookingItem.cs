
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CookingItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    //state 1: dragging
    //state 2: not dragging
    bool dragging =false;
    Vector2 oldPosition;
    RectTransform rectTransform;
    Image image;
    Slider slider;
    bool isBeingCooked = false;
    bool isCompletedCooking = false;
    float cookingStartTime = 0; 
    public bool IsBeingCooked
    {
        get {
            return isBeingCooked;
        }
        set
               
        {
            isBeingCooked = value;
        }
    }


    //game managerde eri�ilebilir olmas� i�in property yap�yoruz.
    public bool IsCompletedCooking
    {
        get
        {
            return isCompletedCooking;
        }
        set
        {
            isCompletedCooking = value;
        }
    }
    public string ID;
    public float TimeToCook = 3f;
    public GameManager GameMgr;
    public CookingPlace Place;
    public bool CanBeGrilled = true;
    public void OnPointerDown(PointerEventData eventData) //hangi nesneye t�klarsak o nesnede �al���r, script di�er nesnelerde olsada �al��maz.
    {
        if (isBeingCooked) { return; } //pi�iriliyorsa tepki verme

        transform.SetAsLastSibling(); //t�klad���m�z nesneyi hiyerar�ide en alta al�r b�ylece ekranda en �stte g�r�r�z.
        oldPosition = rectTransform.anchoredPosition;
        dragging = true;
        image.raycastTarget = false; //elimize ald���m�zda raycasti kapat
       GameMgr.ItemAtHand = this; //buradaki this, CookingItem'� temsil ediyor.
       

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isBeingCooked) { return; } //pi�iriliyorsa tepki verme

        //itemi b�rakt���mda bir oca��n �zerinde miyim? e�er �zerindeysem geri d�nme.
        if (GameMgr.ActivePlace!=null && GameMgr.ActivePlace.ItemPlaced == null && !GameMgr.ItemAtHand.IsCompletedCooking) //oca�� �st�nde olup olmad���n� anlamak i�in yazd�k. Ba�ka item konulmam��sa kabul edecek-null check-
        {
            //evet oca�a koyal�m
            GameMgr.PutItemAtHandToActivePlace();
           
        }
        else
        {
            //mouse ile t�klamay� b�rakt���n�zda �al��an �zel bir komut
            rectTransform.anchoredPosition = oldPosition;
            
            GameMgr.ItemAtHand = null; //elimizi fareden �ekti�imizde, elimizde bir�ey yok dememiz gerekiyor.
            
        }
        dragging = false;
        image.raycastTarget = true; //elimize ald���m�zda raycasti a�
    }

    private void Awake()
    {
        //cache
        rectTransform = GetComponent<RectTransform>();   
        image = GetComponent<Image>();
        transform.Find("Slider");
        
    }


    private void Start()
    {

        slider = transform.Find("Slider").GetComponent<Slider>();

    }


    private void Update()
    {     
        if (dragging)
        {
            //drag the item          
            rectTransform.anchoredPosition = Input.mousePosition;            
        }
        if (isBeingCooked) //sadece pi�me state'inde ise slider ilerleyecek.
        {
            //slider ilerletme form�l�: �imdiki zaman - ba�lang�� zaman� / pi�me s�resi
            slider.value = (Time.time - cookingStartTime) / TimeToCook;
            if (Time.time - cookingStartTime > TimeToCook) //ge�en s�re, pi�irme s�resinden b�y�kse
            {
                //pi�mi� demektir.
                isBeingCooked= false;
                isCompletedCooking=true;
                slider.gameObject.SetActive(false);
            }


        }
    }
    

public void StartCooking()
    {
        isBeingCooked = true;
        cookingStartTime = Time.time; 
        //1. Slider'� g�r�n�r yap.
        //2. state degi�tir (slider'in dolmas�n� sa�la)
        //3. pi�me s�resi bitti�inde state de�i�tir ve slider'i gizle
       slider.gameObject.SetActive(true); ;

    }
}
