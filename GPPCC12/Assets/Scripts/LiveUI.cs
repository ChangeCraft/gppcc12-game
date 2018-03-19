using UnityEngine;

public class LiveUI : MonoBehaviour {

    [SerializeField]
    private GameObject livePrefab;

    public void SetLives (int _lives)
    {
        for (int i = 0; i < _lives; i++)
        {
            Instantiate(livePrefab, transform, false);
        }
    }

    public void SubtractALife ()
    {
        if(transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);
    }

}
