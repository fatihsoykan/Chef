using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // encapsulation


    //1. d��ar�dan eri�ilemeyen bir de�i�ken var
    private CookingItem itemAtHand = null;
    private CookingPlace activePlace = null;
    private Dish activeDish= null;  

    /* 1. ALTERNAT�F
  

    // 2. setter
    public void SetItemAtHand(CookingItem item)
    {
        itemAtHand = item;
    }

    //3. getter
    public CookingItem GetItemAtHand()    {return itemAtHand;}

    */


    // 2. ALTERNAT�F
    // C#'a �zel olarak PROPERTY yap�s� var.
    public CookingItem ItemAtHand
    {
        get //elimizdekini ��renme yeri
        {
            return itemAtHand;
        }

        set //elimize alma yeri
        {
            if (value == null)
            {
                Debug.Log("elimizdeki item'� b�rakt�k:");
            }
            else
            {
                Debug.Log("elimize ald���m�z item:" + value.name);
              
            }
            itemAtHand = value;
        }
        }

        public CookingPlace ActivePlace
    {
        get
        {
            return activePlace; 
        }
        set
        {
           activePlace= value;  
        }

    }


    public Dish ActiveDish
    {
        get
        {
            return activeDish;
        }
        set
        {
            activeDish = value;
        }

    }
    public void PutItemAtHandToActivePlace()
    {
        ItemAtHand.GetComponent<RectTransform>().anchoredPosition = ActivePlace.GetComponent<RectTransform>().anchoredPosition;
        ItemAtHand.StartCooking();
        ItemAtHand.Place = activePlace;
        ActivePlace.ItemPlaced = ItemAtHand;

        ItemAtHand = null;
        ActivePlace = null;
      

    }
}
