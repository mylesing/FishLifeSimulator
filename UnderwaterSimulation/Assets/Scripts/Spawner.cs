using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // prefabs associated with fish
    public GameObject predator;
    public GameObject prey;

    // number of fish upon starting
    public float num_pred;
    public float num_prey;
    public float growth_rate;
    public float prey_destruction_rate;
    public float predator_death_rate;
    public float predator_consumption_rate;

    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (timer % 50 == 0) {
            // clear all previous fish
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("fish");
            foreach(GameObject obj in allObjects) {
                Destroy(obj);
            }

            // multiply by 10 and round up
            float pred_count = Mathf.Round(10.0f * num_pred);
            float prey_count = Mathf.Round(10.0f * num_prey);

            // instantiate predators
            for (int i = 0; i < pred_count; ++i) {
                Vector3 position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                Instantiate(predator, position, Quaternion.identity);
            }

            // instantiate prey
            for (int i = 0; i < prey_count; ++i) {
                Vector3 position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
                Instantiate(prey, position, Quaternion.identity);
            }

            // test
            Debug.Log("Predators: " + pred_count + " and Prey: " + prey_count);

            // update number of predators and prey using the Lotka-Volterra equations
            float A = growth_rate;
            float B = prey_destruction_rate;
            float C = predator_death_rate;
            float D = predator_consumption_rate;
            // dx/dt = Ax - Bxy
            // dy/dt = -Cy + Dxy
            // x = num prey
            // y = num pred
            float dx = (num_prey *  (A - B * num_pred));
            float dy = ( -1 * num_pred) * (C - D * num_prey);

            num_prey += dx;
            num_pred += dy;

            // clamping
            if (num_prey < 0) num_prey = 0.01f;
            if (num_prey > 10)  num_prey = 10;

            if (num_pred < 0) num_pred = 0.01f;
            if (num_pred > 10)  num_pred = 10;

            // satble point
            if (dx == 0 && dy == 0) {
                num_pred = A / B;
                num_prey = C / D;
            }
            
        }

        timer++;
    }
}
