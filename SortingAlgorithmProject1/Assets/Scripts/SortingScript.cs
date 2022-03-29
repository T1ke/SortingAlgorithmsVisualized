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

    List<int> mergeA;
    List<int> mergeB;
    //List<int> res;
    List<int> ar2;
    List<int> m;
    float time;
    

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
        ar2 = new List<int>();
        m = new List<int>();
        foreach (var item in arrray)
        {
            ar2.Add(item);
        }
        switch (sortingAlgorithm)
        {
            case "quicksort":
                StartCoroutine( quicksort(arrray, 0, end));

                //updateTilePositions();
                break;
            case "bubblesort":
                StartCoroutine(bubbleSort(arrray, 0, end));
                break;
            case "bogosort":
                StartCoroutine(bogoSort(arrray));
                
                break;
            case "mergesort":
                
                StartCoroutine(mergeSort(arrray,ar2,0, ar2.Count-1));
                break;
            case "completed":
                break;
            default:
                break;
        }
    }

    IEnumerator mergeSort(List<int> arrray,List<int> ar2,int low, int high)
    {
        List<int> nes = new List<int>();
        print("ar2 : ");
        printArr(ar2);
        if(ar2.Count <= 1)
        {
            print("aaaaaaaaaaa");
            yield return ar2;
        }

        else if (low<high)
        {
            int mid = (low + high) / 2;
            mergeA = new List<int>();
            mergeB = new List<int>();


            for (int ia = 0; ia < mid; ia++)
            {
                mergeA.Add(ar2[ia]);
            }
            yield return new WaitForSeconds(0.001f);
            for (int ib = mid; ib < ar2.Count; ib++)
            {
                mergeB.Add(ar2[ib]);
            }
            yield return new WaitForSeconds(0.001f);

            StartCoroutine(mergeSort(arrray, mergeA, low, mid));
            yield return new WaitForSeconds(0.001f);

            StartCoroutine( mergeSort(arrray, mergeB, mid + 1, high));
            yield return new WaitForSeconds(0.001f);

            nes = merge(arrray, mergeA, mergeB);
            foreach (var item in nes)
            {
                print("item: " + item);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    //    IEnumerator merge(List<int>arrray, List<int>a, List<int>b, int mid,int low, int high)
    List<int> merge(List<int>a, List<int> mergeA, List<int> mergeB)
    {
        List<int> res = new List<int>();// result.Clear();
        
        int indexB = 0;
        int indexA = 0;
        int indexTot = 0 ;
        print("acount")
            ;printArr(mergeA);print("mergb");printArr(mergeB);
        while (indexA < mergeA.Count && indexB < mergeB.Count )
        {
            print("arrindexA " + mergeA[indexA] + " ArrindexB " + mergeB[indexB] + " indexA " + indexA + " indedx " + indexB);

            if (mergeA[indexA] < mergeB[indexB])
            {
                print("mergeA " + mergeA[indexA]);
                res.Add(mergeA[indexA]);
                mergeA.RemoveAt(indexA);
                indexA++;

            }
            else
            {
                print("mergeb[indewxb] " + mergeB[indexB]);

                res.Add(mergeB[indexB]);
                mergeB.RemoveAt(indexB);
                indexB++;


            }
            indexTot++;
        }
        while (indexA < mergeA.Count)
        {
            print("indexToTA: " + indexTot);
            res.Add(mergeA[indexA]);
            mergeA.RemoveAt(indexA);
            indexA++;
            indexTot++;
        }
        while (indexB < mergeB.Count)
        {
            res.Add(mergeB[indexB]);
            mergeB.RemoveAt(indexB);
            indexB++;
            indexTot++;
        }
      
        print("Muua");
        printArr(res);
        for(int ih= 0; ih < indexTot; ih++)
        {
            print(a[ih]);
            arrray[ih] = res[ih];
            print(res[ih]);
            print("\n\n\n\n");
        }
        //
        //yield return new WaitForEndOfFrame();
        print("arrayt count" + a.Count);
        return res;
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
