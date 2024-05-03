using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class MLTesting : MonoBehaviour
{
    public NNModel modelSourece;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var model = ModelLoader.Load(modelSourece);
        var worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);

        var inputTensor = new Tensor(1, 2, new float[2] { 0, 0 });
        worker.Execute(inputTensor);

        var output = worker.PeekOutput();
        // classification model
        if (output[0] < 0.5)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(0, 0.5f, 0);
        }
        else
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(0, 1.5f, 0);
        }
        print("This is the output: " + (output[0] < 0.5 ? 0 : 1));

        inputTensor.Dispose();
        output.Dispose();
        worker.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
