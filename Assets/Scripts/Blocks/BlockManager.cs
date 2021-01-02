using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Settings")]
    [Header("Blocks")]
    public GameObject redBlock;
    public int amountOfRedBlocks = 1;
    
    public GameObject greenBlock;
    public int amountOfGreenBlocks = 3;

    public GameObject blueBlock;
    public int amountOfBlueBlocks = 2;

    [Header("Difficulty")]
    public float timeToActivateBlock = 1.5f;

    [Header("Data")]
    public List<GameObject> blocks;
    public List<int> blocksAmount;
    public List<GameObject> blockTypes;
    public int totalAmountOfBlocks;
    public Vector2 rightEdgeOfView;
    public Vector2 maxLocationToSpawn;
    public float timer;
 

    void Start()
    {
        rightEdgeOfView = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.0f)); //location of right edge of screen 
        maxLocationToSpawn = Camera.main.ViewportToWorldPoint(new Vector2(1.0f, 0.5f)); //location of right edge of screen
        initBlocks();
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeToActivateBlock)
        {
            timer = 0;
            activateRandomBlock();
        }
    }


    public GameObject createNewBlock(GameObject oldBlock)
    {
        Vector2 newPosition = createRandomInBoundsLocation(oldBlock.transform.localScale);
        GameObject newBlock = Instantiate(oldBlock, newPosition, Quaternion.identity) as GameObject;
        newBlock.transform.parent = oldBlock.transform.parent;
        return newBlock;
    }

    private Vector2 createRandomInBoundsLocation(Vector2 blockScale)
    {
        float y = Random.Range(maxLocationToSpawn.y - blockScale.y / 2, rightEdgeOfView.y + blockScale.y / 2);
        return new Vector2(rightEdgeOfView.x + blockScale.x / 2, y);
    }

    private void createMultipleBlocks(GameObject blockType, int amountOfBlocks)
    {
        for(int i = 0; i < amountOfBlocks; ++i)
        {
            blocks.Add(createNewBlock(blockType));
        }
    }

    private void initBlocks()
    {
        //init block amounts
        totalAmountOfBlocks = amountOfRedBlocks + amountOfGreenBlocks + amountOfBlueBlocks;
        blocksAmount = new List<int>();
        blocksAmount.Add(amountOfRedBlocks);
        blocksAmount.Add(amountOfGreenBlocks);
        blocksAmount.Add(amountOfBlueBlocks);

        //init block types
        blockTypes.Add(redBlock);
        blockTypes.Add(greenBlock);
        blockTypes.Add(blueBlock);

        //init blocks, by amount of types
        blocks = new List<GameObject>();
        for(int i = 0; i < blocksAmount.Count; ++i)
        {
           createMultipleBlocks(blockTypes[i], blocksAmount[i]);
        }        
    }

    private void activateBlockWithSpeed(int blockId, float blockSpeed)
    {
        //change block's speed
        BlockLogic bl = blocks[blockId].GetComponent<BlockLogic>();
        if (!bl.blockEnabled)
        {
            bl.movementSpeed = blockSpeed;
            bl.blockEnabled = true;
        }
    }


    private void activateBlock(int blockId)
    {
        //use block's previous speed
        BlockLogic bl = blocks[blockId].GetComponent<BlockLogic>();
        if (!bl.blockEnabled)
        {
            bl.blockEnabled = true;
        }
    }

    private void activateRandomBlock()
    {
        //choose a random block. if block is already activated - do nothing (for difficulty curve - the harder it is the more unlikely it is to get harder)
        int blockId = Mathf.RoundToInt(Random.Range(0, blocks.Count));
        activateBlock(blockId);
    }

}
