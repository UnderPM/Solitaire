using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{

    public Sprite cardFront;
    public Sprite cardBack;

    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Solitaire solitaire;
    private UserInput userInput;
    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Solitaire.GenerateDeck();
        solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<UserInput>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
        int i = 0;

        foreach(string card in deck)
        {
            if (this.name == card)
            {
                cardFront = solitaire.sprites[i];
                break;
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectable.faceUp==true)
        {
            spriteRenderer.sprite = cardFront;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
        if (userInput.slot1)
        {
            if (name == userInput.slot1.name && selectable.faceUp)
            {
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
