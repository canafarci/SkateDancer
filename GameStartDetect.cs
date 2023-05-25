using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartDetect : MonoBehaviour
{
    PathFollower pathfollower;

    private void Awake() 
    {
        pathfollower = GetComponent<PathFollower>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            pathfollower.gameStarted  = true;
            this.enabled = false;
        }
            
    }
}
