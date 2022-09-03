using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverIfFruitTouchGround : MonoBehaviour
{
    public int maxNumberOfError = 3; //after 3 uncut fruits game will end
    private int currentNumberOfError = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            currentNumberOfError++;
            if(currentNumberOfError == maxNumberOfError)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0); //refreshing the scene
            }

            Destroy(collision.gameObject);
        }
    }
}
