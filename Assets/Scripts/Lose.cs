using UnityEngine;

public class Lose : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      FindObjectOfType<SceneLoader>().LoadGameOver();
   }
}
