using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public string itemName;

    public string itemID;

    public string descriptionText;

    public Sprite itemSprite;
}
public class CollectablesManager : MonoBehaviour
{

    public Item item;

    public int hpBUFF;
    public float msBUFF;
    public float asBUFF;


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {

            GameManager.IncreaseMS(msBUFF);
            GameManager.HealPlayer(hpBUFF);
            //ceva
            Destroy(gameObject);
        }
    }
}
