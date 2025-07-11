using UnityEngine;
using UnityEngine.EventSystems;

public class CookingItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    //state 1: dragging
    //state 2: not dragging
    bool dragging =false;
    Vector2 oldPosition;
    RectTransform rectTransform;
    
    public void OnPointerDown(PointerEventData eventData) //hangi nesneye t�klarsak o nesnede �al���r, script di�er nesnelerde olsada �al��maz.
    {
        //mouse ile t�kland���nda �al��an �zel bir komut

        transform.SetAsLastSibling(); //t�klad���m�z nesneyi hiyerar�ide en alta al�r b�ylece ekranda en �stte g�r�r�z.
        oldPosition = rectTransform.anchoredPosition;
        dragging = true;
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //mouse ile t�klamay� b�rakt���n�zda �al��an �zel bir komut
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
            Debug.Log("t�kland�");
        }
    }
    
















}
