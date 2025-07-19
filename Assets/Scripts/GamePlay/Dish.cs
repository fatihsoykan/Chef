using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dish : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public List<string> AskedIDs;
    public GameManager GameMgr; //game manager'e eri�tik ��nki elimizde hangi malzeme var bilgisini almam�z gerek.
    CookingItem enteredItem;
    public void OnPointerEnter(PointerEventData eventData)
    {

        if (GameMgr.ItemAtHand != null && (GameMgr.ItemAtHand.IsCompletedCooking || !GameMgr.ItemAtHand.CanBeGrilled)) //pi�irme bitmi�se veya pi�irilmiyorsa
        {
            enteredItem = GameMgr.ItemAtHand;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        enteredItem = null; 
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && enteredItem != null)
      {        
        if (enteredItem != null)
        {
            for (int i = 0; i < AskedIDs.Count; i++)
            {
                if (AskedIDs[i] == enteredItem.ID)
                {
                    //taba�a koy
                    for (int c = 0; c < transform.childCount; c++)
                    {
                        if (transform.GetChild(c).name == enteredItem.ID)
                        {
                            transform.GetChild(c).GetComponent<Image>().color = Color.white;
                            break;
                        }
                    }
                    //taba�� bo�alt
                    if (enteredItem.Place != null) //e�erki elimizdeki malzeme bir oca�a konmu�sa oca�� bo�alt�yoruz.
                    {
                        enteredItem.Place.ItemPlaced = null;
                    }
                    //eskisini sill
                    enteredItem.gameObject.SetActive(false);
                }
            }
        }
    }
          }
}
