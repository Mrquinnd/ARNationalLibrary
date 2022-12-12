using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour
{

    [SerializedField]

    private GameObject placedPrefab;

    public GameObject PlacedPrefab;
    {
        get 
        {
            return placedPrefab;
        }

        set 
        {
            return placedPrefab = value; 
        }
        
    }

    private ARRaycastManager arRaycastManager{

    }

    void Awake() {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    //Gets position based on user touching the screen. 
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        
        touchPosition = 0;

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPosition(Out Vector2 touchPosition ))
            return;


        if(arRaycastManager.Raycast)
    }

    static List<Raycast
}
