using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializeEdgeCount;
    public MazeRoom room;

    public void Initialize(MazeRoom room){
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    public bool IsFullyInitialized {
        get {
            return initializeEdgeCount == MazeDirections.Count;
        }
    }

    public MazeCellEdge GetEdge(MazeDirection direction){
        return edges[(int)direction];
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge){
        edges[(int)direction] = edge;
        initializeEdgeCount += 1;
    }

    public MazeDirection RandomUninitializedDirection{
        get{
            int skips = Random.Range(0, MazeDirections.Count - initializeEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++){
                if (edges[i] == null){
                    if (skips == 0){
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("Mazecell has no uninitialized directions left.");
        }
    }
}
