using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root == GameObject.Find("Prometheus").transform)
        {
            Debug.Log("🪙 COIN COLLECTED!");

            CoinManager.Instance.CollectCoin();

            Destroy(gameObject);
        }
    }
}