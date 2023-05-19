// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CoinCollector : MonoBehaviour
// {
//     private int coins = 0;

//     [SerializeField] private Text coinsText;

//     private void OnTriggerEnter2D(Collider2D collision)
//     {
//         if (collision.gameObject.CompareTag("Coin"))
//         {
//             Destroy(collision.gameObject);
//             coins++;
//             coinsText.text = "Coins: " + coins;
//         }
//     }
// }
