using UnityEngine;
using UnityEngine.EventSystems;

public class CookingPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager GameMgr; //gamemanager ile ba�lant�s� olmal�
    public CookingItem ItemPlaced //her bir oca��n �zerinde hangi itemi koydu�umuza dair bir kay�t tutmam�z gerek. Oca�a konmu� item kayt�n� tutacak. Bunun i�in encapsualtion yapmam�z laz�m.
    {
        get
        {
            return itemPlaced;
        }
        set
        {
            itemPlaced = value; //setlendi�i zaman otomatik olarak onpointerexit'i �al��t�rabiliriz.
            green.gameObject.SetActive(false);
        }
    }


    CookingItem itemPlaced=null;
    //�ncelikle ekledi�imiz green image'lere eri�ip kapatmam�z gerek.
    Transform green;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //fare imleci cookingplace'i yerle�tirdi�imiz nesnenin koordinatlar�na giri� yapt���nda �al���r.
        //e�er ki fare, koordinatlardan i�eri girmi�se, gamemanager'e elinde ne oldu�unuz soraca��z. E�er ki elinde bir�ey varsa null de�ilse, o zaman ye�ili aktif edebiliriz.
        if (GameMgr.ItemAtHand != null && GameMgr.ItemAtHand.CanBeGrilled && ItemPlaced==null) //ItemPlaced==null yazarak grill �zeri doluysa ye�il yanmas�n� engelledik.
        {
            green.gameObject.SetActive(true);
            GameMgr.ActivePlace = this; //item seciliyken tabak aktif.
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //fare imleci cookingplace'i yerle�tirdi�imiz nesnenin koordinatlar�ndan ��k�� yapt���nda �al���r.
        green.gameObject.SetActive(false);
        GameMgr.ActivePlace = null; //�st�nden cekildiginde hi�bir tabak yok demek.

    }

    private void Start()
    {
        green = transform.Find("Green");
        green.gameObject.SetActive(false);  
    }

}
