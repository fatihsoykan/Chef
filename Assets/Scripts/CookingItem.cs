using UnityEngine;
using UnityEngine.EventSystems;

public class CookingItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    //state 1: dragging
    //state 2: not dragging
    bool dragging =false;
    Vector2 oldPosition;
    RectTransform rectTransform;
    
    public void OnPointerDown(PointerEventData eventData) //hangi nesneye týklarsak o nesnede çalýþýr, script diðer nesnelerde olsada çalýþmaz.
    {
        //mouse ile týklandýðýnda çalýþan özel bir komut

        transform.SetAsLastSibling(); //týkladýðýmýz nesneyi hiyerarþide en alta alýr böylece ekranda en üstte görürüz.
        oldPosition = rectTransform.anchoredPosition;
        dragging = true;
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //mouse ile týklamayý býraktýðýnýzda çalýþan özel bir komut
        rectTransform.anchoredPosition = oldPosition;
        dragging = false;
    }
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

     

    }


    private void Start()
    {
        
    }


    private void Update()
    {     
        if (dragging)
        {
            //drag the item          
            rectTransform.anchoredPosition = Input.mousePosition;
            Debug.Log("týklandý");
        }
    }
    
















}
