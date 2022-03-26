using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    public int amount = 10;
    int[] allTiles;
    public GameObject tilePrefab;
    float tileWidth;
    float tileHeightScale;
    // Start is called before the first frame update
    private void Awake()
    {
        allTiles = new int[amount];
        int i = 0;
        int biggest = 1;
        while (i < amount)
        {
            int rand = (int)Random.Range(1, amount);
            if (rand > biggest)
            {
                biggest = rand;
            }
            allTiles[i] = rand;
            i++;
        }
        tileWidth = (float) 1 / amount;
        
        tileHeightScale = (float) 1 / biggest ;

    }
    void Start()
    {
        instantiateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void instantiateTiles()
    {
        // float i = -Camera.main.orthographicSize * 2 + tileWidth * 0.5f ;
        float i = tileWidth * 0.5f;

        foreach (var tile in allTiles)
        {

            Vector3 tilePos = new Vector3(i, 0.5f, 10f);

            //tilePos = Camera.main.ViewportToScreenPoint(tilePos);
            Instantiate(tilePrefab, Camera.main.ViewportToWorldPoint(tilePos), Quaternion.identity);

            // Vector3 tilePos = Camera.main.ViewportToScreenPoint(new Vector3(i, 0, 0));
            //Instantiate(tilePrefab, tilePos,Quaternion.identity);
            float scaleX = Camera.main.orthographicSize * tileWidth;
            float scaleY = Camera.main.orthographicSize * 2 * tile * tileHeightScale;
            tilePrefab.transform.localScale = new Vector3(scaleX, scaleY, 0);
           
            //tilePrefab.transform.position = Camera.main.ViewportToScreenPoint(tilePos);
          //  tilePrefab.transform.position = Camera.main.ViewportToScreenPoint(new Vector3(i, 0, 0));
            i += tileWidth;

        }
    }
}
