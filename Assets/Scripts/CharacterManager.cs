using UnityEngine;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public Pins pinsDB;
    public static int selection = 0;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;

    private void Start()
    {
        updateCharacter();
    }

    void updateCharacter(){
        Pin current = pinsDB.getPin(selection);
        sprite.sprite = current.prefab.GetComponent<SpriteRenderer>().sprite;
        nameLabel.SetText(current.name);
    }

    public void next(){
        int numberPins = pinsDB.getCount();
        Debug.Log("Count is " + pinsDB.getCount());
        if(selection < numberPins - 1){
            selection = selection + 1;
        }else{
            selection = 0;
        }
        updateCharacter();
    }

    public void previous(){
        if (selection > 0){
            selection = selection - 1;
        }else{
            selection = pinsDB.getCount() -1;
        }
        updateCharacter();
    }
}
