
using System.Collections.Generic;
using UnityEngine;

public class ValuesManager 
{

    public static List<Vector2> getLevel(int level)
    {
        //Vector2(TIME_OF_DEPLOYMENT,TYPE_OF_SHIP)
        List<Vector2> orders = new List<Vector2>();
        switch (level)
        {
            case 0:
                float decreasing_amount = .25f;
                for (int i = 0; i < 6; i++)
                {
                    orders.Add(new Vector2(i / decreasing_amount, 0));
                        decreasing_amount += .25f;
                    //orders.Add(new Vector2(i/ decreasing_amount, 0));
                }
                for (int i = 3; i < 10; i++)
                {
                    orders.Add(new Vector2(i, (int)(Random.value*3)));
                }
                return orders;
            case 1:
                for (int i = 1; i < 20; i++)
                {
                    orders.Add(new Vector2((i / 2), (int)(Random.value * Random.value * 4)));
                }
                for (int i = 10; i < 20; i++)
                {
                    orders.Add(new Vector2(i, (int)(Random.value * 5)));
                }
                return orders;
            case 2:
                for (int i = 1; i < 50; i++)
                {
                    orders.Add(new Vector2(i / 10, (int)(Random.value * 2)));
                }
                orders.Add(new Vector2(5.5f, 6));
                return orders;
            case 3:
                orders.Add(new Vector2(0, 11));
                return orders;
            case 4:
                for (int i = 0; i < 500; i++)
                {
                    orders.Add(new Vector2(i/25, (int)(Random.value * 12f * Random.value)));
                }
                return orders;


        }

        return null;

    }

    public static float getScale(int ship_type)
    {
        if (ship_type == 0 || ship_type == 1)
        {
            return 1;
        }
        else if (ship_type == 1)
        {
            return .75f;
        }
        else
        {
            return .325f;
        }            
    }
    public static float getSpeed(int ship_type)
    {
        switch(ship_type)
        {
            case 0:
                return 10;
            case 1:
                return 8;
            case 2:
                return 6;
            case 3:
                return 5;
            case 4:
                return 4;
            case 5:
                return 3;
            case 6:
                return 3;
            case 7:
                return 2;
            case 8:
                return 1.5f;
            case 9:
                return 1.25f;
            case 10:
                return 1;
            case 11:
                return .8f;
            case 12:
                return .75f;
            case 13:
                return .75f;
        }
        return -1;
    }
    public static float getLife(int ship_type)
    {
        switch (ship_type)
        {
            case 0:
                return 5;
            case 1:
                return 8;
            case 2:
                return 10;
            case 3:
                return 15;
            case 4:
                return 25;
            case 5:
                return 50;
            case 6:
                return 75;
            case 7:
                return 100;
            case 8:
                return 150;
            case 9:
                return 200f;
            case 10:
                return 225;
            case 11:
                return 250;
            case 12:
                return 275;
            case 13:
                return 300;
        }
        return -1;
    }


}
