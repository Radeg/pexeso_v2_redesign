using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour
{
    public GameObject card;
    public Sprite defaultCardSprite;
    public Sprite animalCardSprite;
    public AnimationClip flipAnimation;
    public AnimationClip flipBackAnimation;
    public SoundManager soundManager;
    public static bool isClickable;

    private SpriteRenderer myRenderer;
    private Animator anim;
    private int flipCardHash = Animator.StringToHash("flipCard");
    private int flipCardBackHash = Animator.StringToHash("flipCardBack");

    void Start ()
    {
        anim = GetComponent<Animator>();
        flipAnimation = GetComponent<AnimationClip>();
        flipBackAnimation = GetComponent<AnimationClip>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        isClickable = true;
    }

    void Update ()
    {
        int activeCount = GameObject.FindGameObjectsWithTag("Active").Length;

        if (activeCount == 2)
        {
            isClickable = false;
            MatchingCards();
            StartCoroutine(FlipCardBack());
        }
    }

    void OnMouseDown ()
    {
        if (card.tag == "Untagged" && isClickable == true)
        {
            anim.SetBool(flipCardBackHash, false);
            anim.SetBool(flipCardHash, true);
            card.tag = "Active";
            soundManager.PlaySound(1);
        }
    }

    IEnumerator FlipCardBack()
    {
        yield return new WaitForSeconds(0.7f);
        GameObject[] activeCards = GameObject.FindGameObjectsWithTag("Active");
        foreach (GameObject activeCard in activeCards)
        {
            activeCard.GetComponent<Animator>().SetBool(flipCardBackHash, true);
            activeCard.GetComponent<Animator>().SetBool(flipCardHash, false);
            activeCard.tag = "Untagged";
        }
        yield return new WaitForSeconds(0.6f);
        isClickable = true;
    }

    void MatchingCards()
    {
        GameObject[] sameCards = GameObject.FindGameObjectsWithTag("Active");

        for (int i = 0; i < sameCards.Length; i++)
        {
            if (sameCards[0].name == sameCards[1].name)
            {
                soundManager.PlaySound(2);
                Destroy(sameCards[i], 0.6f);
            }
        }
    }

    void FlipCardChangeSprite ()
    {
        myRenderer.sprite = animalCardSprite;
    }

    void FlipCardBackChangeSprite ()
    {
        myRenderer.sprite = defaultCardSprite;
    }
}