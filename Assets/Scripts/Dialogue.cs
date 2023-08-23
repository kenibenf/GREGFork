using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite playerSprite;
    public Sprite NPCSprite;

    public Sprite[] directions;
    public SpriteRenderer spriteRenderer;
    // stringhe che descrivono il discorso
    public string[] sentences;

    // serve per il pop-up di interazione
    public bool isActive;

    // serve per sapere se un dialogo � aperto
    public bool isPlaying;

    private Vector2 playerPosition;
    private Vector2 NPCPosition;

    /**<summary>
     *  serve per trasformare l'array di stringe in una coda,
     *  che potr� essere utilizzata dal DialogueManager
     * </summary>
     */

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        NPCPosition = (Vector2)this.transform.position;
        isActive = false;
        isPlaying = false;
    }

    public Queue<string> GetQueue()
    {
        Queue<string> result = new Queue<string>();
        for (int i = 0; i < sentences.Length; i++)
        {
            result.Enqueue(sentences[i]);
        }

        return result;
    }


    private void Update()
    {
        if (!isPlaying && Input.GetKeyDown(KeyCode.E) && isActive)
        {
            //setActiveSprite(DetectDirection());
            DialogueManager.current.StartDialogue(this);

        }
    }

    private int DetectDirection()
    {
        Vector2 NPCPositionAbs = new Vector2(Mathf.Abs(NPCPosition.x), Mathf.Abs(NPCPosition.y));
        Vector2 playerPositionAbs = new Vector2(Mathf.Abs(playerPosition.x), Mathf.Abs(playerPosition.y));
        Vector2 differencePosition = NPCPositionAbs - playerPositionAbs;
        float angle = Mathf.Atan2(differencePosition.x, differencePosition.y) * 180 / Mathf.PI;

        if (angle > -60 && angle <= 60) return (int)DialogueManager.DirectionsNPC.UP;
        else if (angle > 60 && angle <= 120) return (int)DialogueManager.DirectionsNPC.LEFT;
        else if (angle > -120 && angle <= -60) return (int)DialogueManager.DirectionsNPC.RIGHT;
        else return (int)DialogueManager.DirectionsNPC.DOWN;
    }

    public void setActiveSprite(int index)
    {
        spriteRenderer.sprite = directions[index];
    }

    // attivazione pop-up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isActive = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Player"))
        {
            playerPosition = (Vector2)collider.transform.position;
        }
    }

    // disattivazione pop-up
    private void OnTriggerExit2D(Collider2D collision)
    {
        isActive = false;
    }
}
