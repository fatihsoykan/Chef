using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // encapsulation


    //1. dýþarýdan eriþilemeyen bir deðiþken var
    private CookingItem itemAtHand = null;
    private CookingPlace activePlace = null;
    private Dish activeDish= null;  

    /* 1. ALTERNATÝF
  

    // 2. setter
    public void SetItemAtHand(CookingItem item)
    {
        itemAtHand = item;
    }

    //3. getter
    public CookingItem GetItemAtHand()    {return itemAtHand;}

    */


    // 2. ALTERNATÝF
    // C#'a özel olarak PROPERTY yapýsý var.
    public CookingItem ItemAtHand
    {
        get //elimizdekini öðrenme yeri
        {
            return itemAtHand;
        }

        set //elimize alma yeri
        {
            if (value == null)
            {
                Debug.Log("elimizdeki item'ý býraktýk:");
            }
            else
            {
                Debug.Log("elimize aldýðýmýz item:" + value.name);
              
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
