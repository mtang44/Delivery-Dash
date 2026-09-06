using UnityEngine;

public class CarSpriteManager : MonoBehaviour
{

    public Sprite wDrive;
    public Sprite swDrive;
    public Sprite sDrive;
    public Sprite seDrive;
    public Sprite eDrive;
    public Sprite neDrive;
    public Sprite nDrive;
    public Sprite nwDrive;

    public GameObject spriteDisplay;
    private float currentRotation; 
    
    
    public enum direction
    {
        north, northEast, east, southEast, south, southWest, west, northWest
    }; 


    public direction currentDirection { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = transform.eulerAngles.y % 360;
        // Debug.Log("Y: " + currentRotation);        
        checkCarDirection();
        setCarSprite();
    }

    // 90 west, 0 south, 270 east, 180 north 
    // 
    void checkCarDirection()
    {
        if(currentRotation >= 345 || currentRotation < 29)
        {
            currentDirection = direction.south;
        }
        else if(currentRotation >= 30 && currentRotation <= 75)
        {
            currentDirection = direction.southEast;
        }
        else if(currentRotation >= 76 && currentRotation <= 119)
        {
            currentDirection = direction.east;
        }
        else if(currentRotation >= 120 && currentRotation <= 150)
        {
            currentDirection = direction.northEast;
        }
        else if(currentRotation >= 151 && currentRotation <= 209)
        {
            currentDirection = direction.north;
        }
        else if(currentRotation >= 210 && currentRotation <= 240)
        {
            currentDirection = direction.northWest;
        }
        else if(currentRotation >= 241 && currentRotation <= 285)
        {
            currentDirection = direction.west;
        }
        else 
        {
            currentDirection = direction.southWest;
        }

    }
    void setCarSprite()
    {
        switch (currentDirection)
        {
            case direction.north:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = nDrive;
                break;
            case direction.northEast:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = neDrive; 
                break;
            case direction.east:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = eDrive;
                break;
            case direction.southEast:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = seDrive;
                break;

            case direction.south:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = sDrive;
                break;

            case direction.southWest:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = swDrive;
                break;
            case direction.west:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = wDrive;
                break;
            case direction.northWest:
                spriteDisplay.GetComponent<SpriteRenderer>().sprite = nwDrive;
                break;
            
            default:
                Debug.Log("Unknown status.");
                break;
        } ;
    }
}
