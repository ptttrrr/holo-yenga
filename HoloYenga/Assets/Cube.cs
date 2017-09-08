using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Cube : MonoBehaviour {

    private Color[] colors = new Color[8];
    private Color currentColor;


    // Use this for initialization
    void Start () {

        colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.black;
        colors[4] = Color.yellow;
        colors[5] = Color.magenta;
        colors[6] = Color.blue;
        colors[7] = Color.gray;

        currentColor = colors[0];

        GenerateCubes(0.3f, true);

        GenerateCubes(0.6f, false);

        GenerateCubes(0.9f, true);

        GenerateCubes(1.2f, false);

        GenerateCubes(1.5f, true);

        GenerateCubes(1.8f, false);

        GenerateCubes(2.1f, true);

        GenerateCubes(2.4f, false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateCubes(float spawnHeightY, bool xAxis)
    {
        // 
        float j = 0;

        for (int i = 0; i < 3; i++)
        {
            j = j + 0.35f;
            CreateCube(j, spawnHeightY, xAxis);
        }
        
    }

    // space to set cubes side by side, spawnYspace for spawn height, xAxis to set the "zigzag" pattern
    void CreateCube(float space, float spawnYspace, bool xAxis)
    {

        // Create object position and size
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
       
        if (xAxis)
        {
            cube.transform.position = new Vector3(0, spawnYspace, space);
            cube.transform.localScale = new Vector3(1f, 0.25f, 0.33f);
        }
        else
        {
            cube.transform.position = new Vector3(space - 0.66f, spawnYspace, 0.66f);
            cube.transform.localScale = new Vector3(0.33f, 0.25f, 1f);
        }
           
        // Get new color for the cube, avoid last used color
        Color color = ColorRandomizer(currentColor);
        cube.GetComponent<Renderer>().material.color = color;
        currentColor = color;

        // Give cube physics
        Rigidbody gameObjectsRigidBody = cube.AddComponent<Rigidbody>();

        // Attach HandDraggable script to cube
        cube.AddComponent<HandDraggable>();
    }

    // Return a random color as long as it's not the last one used.
    Color ColorRandomizer(Color lastColor)
    {
        Color nextColor = lastColor;

        do
        {
            nextColor = colors[Random.Range(0, colors.Length)];

        } while (nextColor == lastColor);

        return nextColor;
    }
}
