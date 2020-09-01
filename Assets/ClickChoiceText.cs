using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickChoiceText : MonoBehaviour, IPointerClickHandler
{   
    [SerializeField] int choiceNumber;
    // EventTrigger.Entry entry;
    //EventTrigger trigger;

    // Start is called before the first frame update
    void Start()
    {
        // EventTrigger trigger = GetComponent<EventTrigger>();
        // EventTrigger.Entry entry = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerDown;
        // entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        // trigger.triggers.Add(entry);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.parent.parent.gameObject.GetComponent<RestfulBehavior>().GetStateAccordingToInput(choiceNumber);
        Debug.Log("hey");
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }

    // void OnMouseDown()
    // {
    //     Debug.Log("click 1");
    // }

    // void OnPointerDownDelegate(PointerEventData a) {
    //     Debug.Log(a);
    // }
}
