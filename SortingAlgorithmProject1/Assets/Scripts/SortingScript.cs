using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    List<int> ar2;
    List<int> m;
    float time;
    int[] arrrray;

    // Start is called before the first frame update
    private void Awake()
    {
        values = new List<int>();
        listOfElems = new List<int>();
        //res = new List<int>();
        
        for (int i = 1;i<=amount;i++)
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
      /*  if(sortingAlgorithm != "completed")
        {
            time += Time.deltaTime;
            print(time.ToString());
        }*/

        updateTilePositions();
    }
    void Start()
    {
        arrray = instantiateTiles();
        arrrray = arrray.ToArray();
        switch (sortingAlgorithm)
        {
            case "quicksort":
                StartCoroutine(quicksort(arrray, 0, end));
                //updateTilePositions();
                break;
            case "bubblesort":
                StartCoroutine(bubbleSort(arrray, 0, end));
                break;
            case "bogosort":
                StartCoroutine(bogoSort(arrray));          
                break;
            case "mergesort":
                //StartCoroutine(MergeSort(arrrray)); //idk easy algorithm to implement but cant visualize it yet
                arrray = MergeSort(arrray.ToArray()).ToList();       //immediately sorts using the mergesort algorithm...       
                break;
            case "completed":
                break;
            default:
                break;
        }
    }

   /* IEnumerator MergeSort(int[] arrrray)
    {

        if (arrrray.Length > 1)
        {
            var mergeA = new List<int>();
            var mergeB = new List<int>();
            
            for (int i = 0; i < arrrray.Length; i++)
            {
                if (i % 2 != 0)
                {
                    mergeA.Add(arrrray[i]);
                }
                else
                {
                    mergeB.Add(arrrray[i]);
                }
            }
            mergeA= StartCoroutine( MergeSort(mergeA.ToArray()));
            yield return MergeSort(mergeB.ToArray());
            // List<int> res = new List<int>();// result.Clear();
            //yield return Merge(arrrray,mergeA, mergeB) ;
            List<int> res = new List<int>();
            while (mergeA.Count > 0 && mergeB.Count > 0)
            {
                if (mergeA.First() <= mergeB.First())
                {
                    res.Add(mergeA.First());
                    mergeA.RemoveAt(0);
                }
                else
                {
                    res.Add(mergeB.First());
                    mergeB.RemoveAt(0);
                }
            }
            while (mergeA.Count > 0)
            {
                res.Add(mergeA.First());
                mergeA.RemoveAt(0);
            }
            while (mergeB.Count > 0)
            {
                res.Add(mergeB.First());
                mergeB.RemoveAt(0);
            }
           
        }
        else
        {
            yield return arrrray;
        }
    }

    IEnumerator Merge(int[] arrrray,List<int> mergeA, List<int> mergeB)
    {
        List<int> res = new List<int>();
        while (mergeA.Count > 0 && mergeB.Count > 0)
        {
            if (mergeA.First() <= mergeB.First())
            {
                res.Add(mergeA.First());
                mergeA.RemoveAt(0);
            }
            else
            {
                res.Add(mergeB.First());
                mergeB.RemoveAt(0);
            }
        }
        while (mergeA.Count > 0)
        {
            res.Add(mergeA.First());
            mergeA.RemoveAt(0);
        }
        while (mergeB.Count > 0)
        {
            res.Add(mergeB.First());
            mergeB.RemoveAt(0);
        }
        arrrray = res.ToArray();
        yield return new WaitForSeconds(0.001f);

    }*/

    int[] MergeSort(int[] arrray)
    {
        if (arrray.Length <= 1)
        {
            print("aaaaaaaaaaa");
            //yield return arrray;
            return arrray;
        }
        var mergeA = new List<int>();
        var mergeB = new List<int>();
        for(int i = 0; i < arrray.Length; i++)
        {
            if(i%2!=0)
            {
                mergeA.Add(arrray[i]);
            }
            else
            {
                mergeB.Add(arrray[i]);
            }
        }
        mergeA = MergeSort(mergeA.ToArray()).ToList(); 
        mergeB = MergeSort(mergeB.ToArray()).ToList();

        return Merge(mergeA,mergeB);
    }

    int[] Merge(List<int> mergeA, List<int> mergeB)
    {
        List<int> res = new List<int>();// result.Clear();

        while (mergeA.Count > 0 && mergeB.Count > 0)
        {
            if(mergeA.First() <= mergeB.First())
            {
                res.Add(mergeA.First());
                mergeA.RemoveAt(0);
            }
            else
            {
                res.Add(mergeB.First());
                mergeB.RemoveAt(0);
            }
        }
        while(mergeA.Count > 0)
        {
            res.Add(mergeA.First());
            mergeA.RemoveAt(0);

        }
        while (mergeB.Count > 0)
        {
            res.Add(mergeB.First());
            mergeB.RemoveAt(0);
        }
        printArr(res);
        return res.ToArray();
    }

    public List<int> instantiateTiles()
    {
        List<int> tileArray = new List<int>();
        tilePrefabArray = new List<GameObject>();
        float i = tileWidth * 0.5f;
        int index = 0;
        print("ALL ELEMTS IN ARRAY");
        printArr(listOfElems);
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

        if (isSorted(arrray))
        {
            print("quicksort done");
            sortingAlgorithm = "completed";
        }

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

    IEnumerator bogoSort(List<int> arr)
    {
        while (!isSorted(arr))
        {
            shuffle(arr);
            yield return new WaitForEndOfFrame();
        }
        
        print("bogosort done");
        sortingAlgorithm = "completed";
    }

    void shuffle(List<int>arr)
    { //for bogosort
        int n = arr.Count;
        int last = n - 1;
        for(int i = 0; i < last; ++i)
        {
            int f = Random.Range(i, n);
            swap(arr, i, f);
        }
    }

    bool isSorted(List<int> arr)
    {
        for(int i =0;i < arr.Count-1;i++)
        {
            if(arr[i] > arr[i+1])
            {
                return false;
            }
        }
        return true;
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
