using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHelper : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    public void GameOver()
    {
        gameController.GameOver();
    }

}
