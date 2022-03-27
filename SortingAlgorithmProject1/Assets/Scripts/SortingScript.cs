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
    public string sortingAlgorithm = "quicksort";
    List<int> aray;
    GameObject[] goArr;
    int start;
    int end;
    int indexPart = 0;
    List<int> values;
    List<int> listOfElems;
    // Start is called before the first frame update
    private void Awake()
    {
        values = new List<int>();
        listOfElems = new List<int>();

        for(int i = 1;i<=amount;i++)
        {
            values.Add(i);
        }
        for(int i =0;i<amount;i++)
        {
            int index = Random.Range(0, values.Count);
            listOfElems.Add(values[index]);
            values.RemoveAt(index);
        }
        goArr = new GameObject[amount];


        start = 0;
        end = listOfElems.Count - 1;
        

        /*
        while (i < amount)
        {
            int rand = (int)Random.Range(1, amount);
            if (rand > biggest)
            {
                biggest = rand;
            }
           
            allTiles[i] = rand;
            i++;
        }*/
        tileWidth = (float) 1 / amount;
        
        tileHeightScale = (float) 1 / amount ;

    }
    void Start()
    {
        aray = instantiateTiles();
        switch (sortingAlgorithm)
        {
            case "quicksort":
                quicksort(aray, start, end);

                //updateTilePositions();
                break;
            default:
                break;
        }
    }

    public List<int> instantiateTiles()
    {
        List<int> tileArray = new List<int>();

        float i = tileWidth * 0.5f;
        int index = 0;
        foreach (var tile in listOfElems)
        {
            Vector3 tilePos = new Vector3(i, 0.5f, 10f);

            Instantiate(tilePrefab, Camera.main.ViewportToWorldPoint(tilePos), Quaternion.identity);

            float scaleX = Camera.main.orthographicSize * tileWidth;
            float scaleY = Camera.main.orthographicSize * 2 * tile * tileHeightScale;
            tilePrefab.transform.localScale = new Vector3(scaleX, scaleY, 0);
            goArr[index] = tilePrefab;
            tileArray.Add(tile);
            index++;
            i += tileWidth;

        }

        return tileArray;
    }

    void quicksort(List<int> aray, int start,int endv)
    {
        if(start >= endv)
        {
            // updateTilePositions(aray);
            print("VALMIES");
            printArr(aray);
            return;
        }

        indexPart = partition(aray, start, endv);
        quicksort(aray, start, indexPart - 1);
        quicksort(aray, indexPart + 1, endv);
        

    }

    int partition(List<int> array, int start, int endv)
    {
        int indexStart = start;
        var indexVal = array[endv];

        for (int i =start;i < end; i++)
        {
            if(array[i] < indexVal)
            {
                
                swap(array, i, indexStart);
                indexStart++;
            }          
        }
        printArr(array);
        swap(array, indexStart, endv);
        return indexStart;
    }

    void swap(List<int> arr, int a, int b)
    {
        var temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;

    }

    void updateTilePositions(int[] aray)
    { //TODO UPDATE TILES AFTER EVERY UPDATE
        GameObject[] taged = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var item in taged)
        {
            Destroy(item);
        }
        float i = tileWidth * 0.5f;

        foreach (var tile in aray)
        {

            Vector3 tilePos = new Vector3(i, 0.5f, 10f);

            Instantiate(tilePrefab, Camera.main.ViewportToWorldPoint(tilePos), Quaternion.identity);

            float scaleX = Camera.main.orthographicSize * tileWidth;
            float scaleY = Camera.main.orthographicSize * 2 * tile * tileHeightScale;
            tilePrefab.transform.localScale = new Vector3(scaleX, scaleY, 0);
            i += tileWidth;

        }

    }

    void printArr(int[] arr)
    {
        string res = "";
        foreach (var item in arr)
        {
            res += item + ", ";

        }
        print(res);
    }
    void printArr(GameObject[] arr)
    {
        string res = "";
        foreach (var item in arr)
        {
            res += item.transform.localPosition.x + ", ";

        }
        print(res);
    }
    void printArr(List<int> arr)
    {
        string res = "";
        foreach (var item in arr)
        {
            res += item + ", ";

        }
        print(res);
    }
}
