using UnityEngine;
using UnityEngine.EventSystems;

public class CookingPlace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameManager GameMgr; //gamemanager ile baðlantýsý olmalý
    public CookingItem ItemPlaced //her bir ocaðýn üzerinde hangi itemi koyduðumuza dair bir kayýt tutmamýz gerek. Ocaða konmuþ item kaytýný tutacak. Bunun için encapsualtion yapmamýz lazým.
    {
        get
        {
            return itemPlaced;
        }
        set
        {
            itemPlaced = value; //setlendiði zaman otomatik olarak onpointerexit'i çalýþtýrabiliriz.
            green.gameObject.SetActive(false);
        }
    }


    CookingItem itemPlaced=null;
    //Öncelikle eklediðimiz green image'lere eriþip kapatmamýz gerek.
    Transform green;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //fare imleci cookingplace'i yerleþtirdiðimiz nesnenin koordinatlarýna giriþ yaptýðýnda çalýþýr.
        //eðer ki fare, koordinatlardan içeri girmiþse, gamemanager'e elinde ne olduðunuz soracaðýz. Eðer ki elinde birþey varsa null deðilse, o zaman yeþili aktif edebiliriz.
        if (GameMgr.ItemAtHand != null && GameMgr.ItemAtHand.CanBeGrilled && ItemPlaced==null) //ItemPlaced==null yazarak grill üzeri doluysa yeþil yanmasýný engelledik.
        {
            green.gameObject.SetActive(true);
            GameMgr.ActivePlace = this; //item seciliyken tabak aktif.
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //fare imleci cookingplace'i yerleþtirdiðimiz nesnenin koordinatlarýndan çýkýþ yaptýðýnda çalýþýr.
        green.gameObject.SetActive(false);
        GameMgr.ActivePlace = null; //üstünden cekildiginde hiçbir tabak yok demek.

    }

    private void Start()
    {
        green = transform.Find("Green");
        green.gameObject.SetActive(false);  
    }

}
