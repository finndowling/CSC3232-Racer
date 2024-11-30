using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public int numPlayers = 4;
    public int numLaps = 3;
    public Car car;
    public Car car1;
    public Car car2;
    public Car car3;
    


    public Player1 humanPlayer;  // Assign in the Inspector
    public Player1 aiPlayer1;    // Assign in the Inspector
    public Player1 aiPlayer2;    // Assign in the Inspector
    public Player1 aiPlayer3;    // Assign in the Inspector
    
    // Start is called before the first frame update
    void Start()
    {
        humanPlayer.AssignCar(car);
        aiPlayer1.AssignCar(car1);
        aiPlayer2.AssignCar(car2);
        aiPlayer3.AssignCar(car3);        
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}
