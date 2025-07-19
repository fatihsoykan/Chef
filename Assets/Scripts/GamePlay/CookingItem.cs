
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


    //game managerde eriþilebilir olmasý için property yapýyoruz.
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
    public void OnPointerDown(PointerEventData eventData) //hangi nesneye týklarsak o nesnede çalýþýr, script diðer nesnelerde olsada çalýþmaz.
    {
        if (isBeingCooked) { return; } //piþiriliyorsa tepki verme

        transform.SetAsLastSibling(); //týkladýðýmýz nesneyi hiyerarþide en alta alýr böylece ekranda en üstte görürüz.
        oldPosition = rectTransform.anchoredPosition;
        dragging = true;
        image.raycastTarget = false; //elimize aldýðýmýzda raycasti kapat
       GameMgr.ItemAtHand = this; //buradaki this, CookingItem'ý temsil ediyor.
       

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (isBeingCooked) { return; } //piþiriliyorsa tepki verme

        //itemi býraktýðýmda bir ocaðýn üzerinde miyim? eðer üzerindeysem geri dönme.
        if (GameMgr.ActivePlace!=null && GameMgr.ActivePlace.ItemPlaced == null && !GameMgr.ItemAtHand.IsCompletedCooking) //ocaðý üstünde olup olmadýðýný anlamak için yazdýk. Baþka item konulmamýþsa kabul edecek-null check-
        {
            //evet ocaða koyalým
            GameMgr.PutItemAtHandToActivePlace();
           
        }
        else
        {
            //mouse ile týklamayý býraktýðýnýzda çalýþan özel bir komut
            rectTransform.anchoredPosition = oldPosition;
            
            GameMgr.ItemAtHand = null; //elimizi fareden çektiðimizde, elimizde birþey yok dememiz gerekiyor.
            
        }
        dragging = false;
        image.raycastTarget = true; //elimize aldýðýmýzda raycasti aç
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
        if (isBeingCooked) //sadece piþme state'inde ise slider ilerleyecek.
        {
            //slider ilerletme formülü: þimdiki zaman - baþlangýç zamaný / piþme süresi
            slider.value = (Time.time - cookingStartTime) / TimeToCook;
            if (Time.time - cookingStartTime > TimeToCook) //geçen süre, piþirme süresinden büyükse
            {
                //piþmiþ demektir.
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
        //1. Slider'ý görünür yap.
        //2. state degiþtir (slider'in dolmasýný saðla)
        //3. piþme süresi bittiðinde state deðiþtir ve slider'i gizle
       slider.gameObject.SetActive(true); ;

    }
}
