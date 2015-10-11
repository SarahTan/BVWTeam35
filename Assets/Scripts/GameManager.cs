using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public static bool gameInProgress = false;

    public FruitChoose fruitChoose;
    public Timer timer;
    public SoundManager soundManager;
    public Animator[] anim = new Animator[2];

    public Player cat;
    public Player dog;

    int winner = -1;
    int loser = -1;

    // Use this for initialization
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGame()
    {
        gameInProgress = true;
        timer.StartTimer();
        cat.currentFruit = fruitChoose.AssignFruit(0);
        dog.currentFruit = fruitChoose.AssignFruit(1);

        Debug.Log("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
    }

    void FruitObtained(Player player)
    {
        Debug.Log("Player" + player + " collected");
        soundManager.PlayCollectFruit(true);
        soundManager.PlayAnimalSpeak(player.animalNum, SoundManager.SPEECH.RIGHT_FRUIT);
        anim[player.animalNum].SetTrigger("Happy");

        player.ChangeCupLevel(true);

        // switch to next cup
        if (player.cupLevel == player.maxCupLevel)
        {
            StartCoroutine(ChangeCups(player));
        }

        player.currentFruit = fruitChoose.AssignFruit(player.animalNum);
        Debug.Log("Cat: " + cat.currentFruit + ", dog: " + dog.currentFruit);
    }

    IEnumerator ChangeCups(Player player)
    {
        yield return new WaitForSeconds(1f);    // change this to right fruit sound duration

        soundManager.PlayBlender();
        soundManager.PlayAnimalSpeak(player.animalNum, SoundManager.SPEECH.CUP_FILLED);

        player.ChangeCupLevel(false);

        // cupcount increase anim

        player.IncreaseScore();
    }


    public void ReceiveInput(int fruit)
    {
        if (gameInProgress)
        {
            if (fruit == cat.currentFruit)
            {
                FruitObtained(cat);

            }
            else if (fruit == dog.currentFruit)
            {
                FruitObtained(dog);

            }
            else
            {
                soundManager.PlayCollectFruit(false);
                Debug.Log("Punishing players");

                cat.ChangeCupLevel(false);
                dog.ChangeCupLevel(false);
            }
        }
    }

    public void TimesUp()
    {
        gameInProgress = false;
        StopAllCoroutines();
        soundManager.PlayEndGame();

        if (cat.score > dog.score)
        {
            winner = 0;
            loser = 1;
        }
        else
        {
            winner = 1;
            loser = 0;
        }

        soundManager.PlayAnimalSpeak(winner, SoundManager.SPEECH.WIN);
        anim[winner].SetBool("Win", true);
        // you win text

        soundManager.PlayAnimalSpeak(loser, SoundManager.SPEECH.LOSE);
        anim[loser].SetBool("Win", false);
        // you lose text
    }
}