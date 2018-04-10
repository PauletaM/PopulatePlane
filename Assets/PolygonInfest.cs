using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonInfest : MonoBehaviour {

    public Transform mesh;
    public float distanceBetweenObjs;
    public GameObject objToInstantiate;    
    private int[][] placementControl;
    private float delay;

    private void Start()
    {
        Infest();
    }

    public void Infest()
    {
        placementControl = new int[50][];
        for (int i = 0; i < 50; i++)
            placementControl[i] = new int[50];        

        PutObj( mesh.transform.position, 25, 25 );
    }

    void PutObj( Vector3 rayPos, int x, int z )
    {
        Ray ray = new Ray(rayPos + new Vector3(0, 1, 0), new Vector3(0, -2, 0));
        if (Physics.Raycast(ray))
        {
            placementControl[x][z] = 1;
            StartCoroutine(InstantiateWithDelay(delay, rayPos));
            delay += 0.002f;
        }
        else
        {
            placementControl[x][z] = -1;
        }    
         
        //right
        if ( ( x + 1 < 50 ) && ( placementControl[ x + 1 ][ z ] == 0 ) )
            PutObj(rayPos + new Vector3(distanceBetweenObjs, 0, 0 ), x + 1, z);

        //left
        if ( ( x - 1 >= 0 ) && ( placementControl[ x - 1 ][ z ] == 0 ) )
            PutObj(rayPos + new Vector3(-distanceBetweenObjs, 0, 0), x - 1, z);

        //up
        if ( ( z + 1 < 50 ) && ( placementControl[ x ][ z + 1 ] == 0 ) )
            PutObj( rayPos + new Vector3( 0, 0, distanceBetweenObjs ), x, z + 1 );

        //down
        if ( ( z - 1 >= 0 ) && ( placementControl[ x ][ z - 1 ] == 0 ) )
            PutObj(rayPos + new Vector3(0, 0, -distanceBetweenObjs), x, z - 1);
                   
        
    }

    IEnumerator InstantiateWithDelay( float delay, Vector3 rayPos )
    {     
        yield return new WaitForSeconds(delay);
        Instantiate(objToInstantiate, rayPos, Quaternion.identity);
    }


}
