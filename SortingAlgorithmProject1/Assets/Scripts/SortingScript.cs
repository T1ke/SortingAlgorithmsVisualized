using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingScript : MonoBehaviour
{
    public int amount = 10;
    
    public GameObject tilePrefab;
    public float xMultiplier = 1f;
    float tileWidth;
    float tileHeightScale;
    public string sortingAlgorithm = "quicksort";
    List<int> arrray;
    List<GameObject> tilePrefabArray;
    //int start;
    int end;
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
        
        end = listOfElems.Count- 1;
        
        tileWidth = (float) 1 / amount;   
        tileHeightScale = (float) 10 / amount ;

    }

    private void Update()
    {
        //printArr(aray);
        updateTilePositions();
    }
    void Start()
    {
        arrray = instantiateTiles();
        switch (sortingAlgorithm)
        {
            case "quicksort":
                StartCoroutine( quicksort(arrray, 0, end));

                //updateTilePositions();
                break;
            case "bubblesort":
                StartCoroutine(bubbleSort(arrray, 0, end));
                break;
            case "completed":
                break;
            default:
                break;
        }
    }

    public List<int> instantiateTiles()
    {
        List<int> tileArray = new List<int>();
        tilePrefabArray = new List<GameObject>();
        float i = tileWidth * 0.5f;
        int index = 0;
        foreach (var tile in listOfElems)
        {
            Vector3 tilePos = new Vector3(i, 0.5f, 10f);

            GameObject prefab = Instantiate(tilePrefab, Camera.main.ViewportToWorldPoint(tilePos), Quaternion.identity);
           // print(prefab.transform.position);
           
            float scaleX = Camera.main.orthographicSize * tileWidth*xMultiplier;
            float scaleY = Camera.main.orthographicSize * 2 * tile * tileHeightScale;

            tilePrefab.transform.localScale = new Vector3(scaleX, scaleY, 0);
            tilePrefabArray.Add(prefab);
            
            tileArray.Add(tile);
            index++;
            i += tileWidth;

        }

        return tileArray;
    }

    IEnumerator quicksort(List<int> arrray, int start, int endv)
    { 
        if (start <= endv)
        {
            int indexStart = start;
            int end = endv;
            int indexVal = arrray[endv];
            
            for (int i = start; i < end; i++)
            {
                if (arrray[i] <= indexVal)
                {
                    swap(arrray, i, indexStart);

                    indexStart++;
                    yield return new WaitForEndOfFrame();
                }
            }
            swap(arrray, indexStart, endv);

          //  printArr(arrray);
           
            StartCoroutine(quicksort(arrray, start, indexStart - 1));
            StartCoroutine(quicksort(arrray, indexStart + 1, endv));

        }
        print("quicksort done");
        sortingAlgorithm = "stop";

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
        swap(array, indexStart, endv);
        return indexStart;
    }

    IEnumerator bubbleSort(List<int> arr, int start, int end)
    {
        for (int i =start; i <arr.Count ;i++)
        {
            for(int j = 0;j<arr.Count-i-1;j++)
            {
                int a = arr[j];
                int b = arr[j+1];
                if(a>b)
                {
                    swap(arr, j, j+1);
                }
                yield return new WaitForEndOfFrame();
            }
        }
        print("bubblesorting completed");
        sortingAlgorithm = "completed";
        //yield return WaitForEndOfFrame();
    }

    void swap(List<int> arr, int a, int b)
    { //swap the places of the 2 elements
        var temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }

    void updateTilePositions()
    {
        int kx = 0;
        foreach (var item in tilePrefabArray)
        {
            item.transform.localScale = new Vector3(item.transform.localScale.x, tileHeightScale*arrray[kx], item.transform.localScale.z);
            kx++;
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
