using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptableCard : ScriptableObject
{

}

public interface ICard   
{
    void useCard();
}

public class Card
{
    public GameObject visual;
    public SelectableCard selectableCard;
    public Card(GameObject _visual)
    {
        visual = _visual;
        selectableCard = visual.GetComponent<SelectableCard>();
    }

    public Card Clone()
    {
        GameObject duplicateObject = Object.Instantiate(visual, visual.transform.parent);

        // Optionally, you can set the position, rotation, and scale of the duplicateObject
        duplicateObject.transform.position = visual.transform.position;
        duplicateObject.transform.rotation = visual.transform.rotation; // No rotation
        duplicateObject.transform.localScale = visual.transform.localScale; // Default scale

        return new Card(duplicateObject);
    }
}
