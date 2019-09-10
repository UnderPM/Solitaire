using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{

    public GameObject slot1;

    private Solitaire solitaire;

    // Start is called before the first frame update
    void Start()
    {
        solitaire = FindObjectOfType<Solitaire>();
        slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                    slot1 = this.gameObject;
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Top"))
                {

                }
                else if (hit.collider.CompareTag("Bottom"))
                {

                }
            }
        }
    }

    void Deck()
    {
        solitaire.DealFromDeck();
    }
    void Card(GameObject selected)
    {
        //if the card is facedown
            //if the card is not blocked
                //flip it over
        //if the card is in deck pile
            //if it isn't blocked
                //select it
        //if card is face up
            //if there is none selected
                //select it
            //if other card is selected
                //if can stack with old card
                    //stack it
                //else
                    //select new card
            //if same card is selected
                //if quick enough = double click
                    //if eligible move to top


        if (slot1 == this.gameObject)
        {
            slot1 = selected;
        }
        else if (slot1 != selected)
        {
            if (Stackable(selected))
            {
                //stack it
                //print("stackable");
                Stack(selected);
                
            }
            else
            {
                //print("FUCK");
                //slot1 = this.gameObject;
                //slot1 = selected;
            }
        }
        else if (slot1 == selected)
        {
            // de-select the card
            slot1 = this.gameObject;
        }
        
        //if card is facedown
        if (!selected.GetComponent<Selectable>().faceUp)
        {



            for (int i = 0; i < 7; i++)
            {
                string name = solitaire.bottoms[i][solitaire.bottoms[i].Count - 1];
                                
                if (selected.name == name)
                {
                    selected.GetComponent<Selectable>().faceUp = true;
                    break;
                }
            }

            
            
            //if card isn't blocked
            /*if (solitaire.bottoms)
            {
                //flip over
                selected.GetComponent<Selectable>().faceUp = true;
            }
            else
            {
                //print("Card Blocked");
            }*/
        }
        else
        {
            //if card isn't blocked
            if (1 == 0)
            {
                //paint it  ̶b̶l̶a̶c̶k̶  yellow
                
            }
        }
    }
    void Top()
    {

    }
    void Bottom()
    {

    }

    bool Stackable(GameObject selected)
    {
        bool slot1IsRed;
        bool selectedIsRed;

        char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        if (slot1.gameObject.name.ToString().Trim(numbers) == "Hearts" || slot1.gameObject.name.ToString().Trim(numbers) == "Diamonds")
        {
            slot1IsRed = true;
        }
        else
        {
            slot1IsRed = false;
        }
        if (selected.gameObject.name.ToString().Trim(numbers) == "Hearts" || selected.gameObject.name.ToString().Trim(numbers) == "Diamonds")
        {
            selectedIsRed = true;
        }
        else
        {
            selectedIsRed = false;
        }
        
        System.Int32.TryParse((slot1.gameObject.name.Substring(slot1.gameObject.name.ToString().Length - 2)), out int slot1Number);
        if (slot1Number == 0)
        {
            System.Int32.TryParse((slot1.gameObject.name.Substring(slot1.gameObject.name.ToString().Length - 1)), out slot1Number);
        }

        System.Int32.TryParse((selected.gameObject.name.Substring(selected.gameObject.name.ToString().Length - 2)), out int selectedNumber);
        if (selectedNumber == 0)
        {
            System.Int32.TryParse((selected.gameObject.name.Substring(selected.gameObject.name.ToString().Length - 1)), out selectedNumber);
        }

        if ((slot1IsRed && !selectedIsRed || !slot1IsRed && selectedIsRed) && selectedNumber - slot1Number == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Stack(GameObject selected)
    {

        string parent1 = slot1.gameObject.transform.parent.name;
        parent1 = parent1.Substring(parent1.Length - 1);
        if (!System.Int32.TryParse(parent1, out int indexParent1)) { indexParent1 = -1; }

        string parent2 = selected.gameObject.transform.parent.name;
        parent2 = parent2.Substring(parent2.Length - 1);
        if (!System.Int32.TryParse(parent2, out int indexParent2)) { indexParent2 = -1; }

        if (0 <= indexParent1 && 0 <= indexParent2)
        {
            solitaire.bottoms[indexParent2].Add(slot1.name);
            solitaire.bottoms[indexParent1].RemoveAt(solitaire.bottoms[indexParent1].IndexOf(slot1.name));
            slot1.gameObject.transform.SetParent(selected.gameObject.transform.parent);
            float yOffset = 0.3f;
            float zOffset = 0.03f;
            slot1.gameObject.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - zOffset);
        }
    }
}
