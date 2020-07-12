using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandlerScript : MonoBehaviour
{
    public FinishLineScript finishLineScript;
    public CheckpointScript[] checkpoints;
    public WindowQuestPonter windowQuestPonter;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (checkpoints.Equals(null))
        {
            GameObject[] tempCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

            checkpoints = new CheckpointScript[tempCheckpoints.Length];
            for (int i = 0; i < tempCheckpoints.Length; i++)
            {
                checkpoints[i] = tempCheckpoints[i].GetComponent<CheckpointScript>();
            }
        }
        finishLineScript.checkpoints = checkpoints;
    }

    // Update is called once per frame
    void Update()
    {
        windowQuestPonter.setTarget(getNextCheckpoint());
    }


    Vector3 getNextCheckpoint()
    {
        foreach (CheckpointScript c in checkpoints)
        {
            if (!c.Passed)
            {
                return c.getPoint();
            }
        }

        return finishLineScript.transform.position;
    }
}
