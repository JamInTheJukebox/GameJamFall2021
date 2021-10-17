using UnityEngine;
using UnityEngine.Events;

public class Item : Interactable
{
    [SerializeField] ItemScriptable itemInformation;
    protected UnityAction<ItemScriptable> interactAction;           // specify what kind of information we are passing in with this action

    private void Start()
    {
        interactAction += MasterUserInterface.instance.ItemUIController.collectItem;
        //eventListener.AddListener();
    }

    public override bool Interact(Vector2 targetPos)        // did we successfully interact with the item?
    {
        if (!TouchToInteract && base.Interact(targetPos))
        {
            return true; // failed to get the item
        }

        // failed to get the item   
        return false;
    }

    public override void executeInteractable()
    {
        interactAction?.Invoke(itemInformation);           // pass in the item we collect!
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TouchToInteract) { return; }
       
        if (collision.gameObject.tag == Tags.PLAYER)
        {
            executeInteractable();
        }
    }
}
