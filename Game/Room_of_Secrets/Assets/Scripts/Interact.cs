using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The script attached to each of the clues that inherits from this Script.
/// The interaction that are same for all the clue objects are handled from here
/// Clue dependent interactions are handled by `interactWithClue` Method
/// 1. How to exit from a clue
/// 2. Displaying the appropriate UI for the Clue
/// 3. How does the player interact with the clue
/// </summary>
public abstract class Interact : MonoBehaviour 
{
    // A UI Panel that displays "Press F to interact"
    // 
    public bool canInteract = false;
    public GameObject preInteract;
    public ClueUI clueUI;
    
    public void Start()
    {
        clueUI = GetComponent<ClueUI>();    
    }

   private void Update()
    {
    }

    /// <summary>
    /// Only show the interact message when the player is facing the collided object.
    /// </summary>
    /// <param name="player">The GameObject representing the player.</param>
    public void ShowPreInteract(GameObject player) {
        float viewingAngleThreshold = 45.0f;
        Vector3 directionToPlayer = gameObject.transform.position - player.transform.position;
        float viewingAngle = Vector3.Angle(player.transform.forward, directionToPlayer);
        if (viewingAngle < viewingAngleThreshold )
        {

            if (preInteract.transform.parent != null)
            {
                Transform current = preInteract.transform.parent.GetComponent<Transform>();
                current.gameObject.SetActive(true);
            }
            preInteract.SetActive(true);
            
            canInteract = true;


        }
    }

    /// <summary>
    /// Hides the interact message when the player is beyond a certain distance from the collider.
    /// </summary>
    /// <param name="player">The GameObject representing the player.</param>
    public void HidePreInteract(GameObject player)
    {
        //Hide only when the player is beyond a certain distance from the collider
        float hideThreshold = 50.0f;
        float distanceFromCollider = Vector3.Distance(player.transform.position, preInteract.transform.position);
        if (preInteract.activeSelf && distanceFromCollider>hideThreshold)
        {
            if (preInteract.transform.parent.gameObject.activeSelf)
            {
                Transform parent = preInteract.transform.parent;
                parent.gameObject.SetActive(false);
            }
            preInteract.SetActive(false);
            canInteract = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowPreInteract(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HidePreInteract(collision.gameObject);
        }
    }

    /// <summary>
    /// Handle what Happens when the F key is pressed from scripts attached to individual clues
    /// This method when called should :
    /// 1. Show the clue title and the clueDescription For example
    /// clueTitle : Sir alexandria's Dead body 
    /// clueDescription : wardrobe password 1234
    /// </summary>
    public abstract void interactWithClue();


}
