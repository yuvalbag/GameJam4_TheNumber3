using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
    public int amountOfRedBlocks = 1;
    public int amountOfBlueBlocks = 2;
    public int amountOfGreenBlocks = 3;
    public GameObject redBlock;
    public GameObject greenBlock;
    public GameObject blueBlock;

    [Header("Data")]
    public GameObject[] blocks;
    public int[] blocksAmount;
    public GameObject[] blockTypes;
    public int totalAmountOfBlocks;
    private Vector2 rightEdgeOfView;
    private Vector2 maxLocationToSpawn;

    void Start()
    {
        rightEdgeOfView = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.0f)); //location of right edge of screen 
        maxLocationToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.3f)); //location of right edge of screen
        initBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject createNewBlock(GameObject oldBlock)
    {
        Vector2 newPosition = createRandomInBoundsLocation();
        GameObject newBlock = Instantiate(oldBlock, newPosition, Quaternion.identity) as GameObject;
        newBlock.transform.parent = oldBlock.transform.parent;
        return newBlock;
    }

    private Vector2 createRandomInBoundsLocation()
    {
        float y = Random.Range(maxLocationToSpawn.y, rightEdgeOfView.y);
        return new Vector2(rightEdgeOfView.x, y);
    }

    private int createMultipleBlocks(GameObject blockType, int amountOfBlocks, int arrayStartIndex)
    {
        for(int i = arrayStartIndex; i < arrayStartIndex + amountOfBlocks; ++i)
        {
            blocks[i] = createNewBlock(blockType);
        }
        return arrayStartIndex + amountOfBlueBlocks + 1; //return the next index to init blocks in
    }

    private void initBlocks()
    {
        //init block amounts
        totalAmountOfBlocks = amountOfRedBlocks + amountOfGreenBlocks + amountOfBlueBlocks;
        blocksAmount = new int[3];
        blocksAmount[0] = amountOfRedBlocks;
        blocksAmount[1] = amountOfGreenBlocks;
        blocksAmount[2] = amountOfBlueBlocks;

        //init blocks, by amount of types
        blocks = new GameObject[totalAmountOfBlocks];
        int tempIndex = 0;
        for(int i = 0; i < blocksAmount.Length; ++i)
        {
            tempIndex = createMultipleBlocks(blockTypes[i], blocksAmount[i], tempIndex);
        }        
    }
}
