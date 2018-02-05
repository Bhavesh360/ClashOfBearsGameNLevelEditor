using System;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    internal List<Building> buildings = new List<Building>();
    public override void SingletonAwake()
    {
       
    }

}
