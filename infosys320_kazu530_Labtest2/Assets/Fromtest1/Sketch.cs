using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    // Put your URL here
    public string _WebsiteURL = "http://infosys320123.azurewebsites.net/tables/TreeSurvey?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Cenotaph[] products = JsonReader.Deserialize<Cenotaph[]>(jsonResponse);

        int totalCubes = products.Length;
        //int totalDistance = 5;
        int i = 0;
        int count = 0;

        //We can now loop through the array of objects and access each object individually
        foreach (Cenotaph product in products)
        {
            
            {

                int X = product.X;
                int Y = product.Y;
                int Z = product.Z;

                Debug.Log("Number of rows : " + products.Length);
                //Example of how to use the object
                Debug.Log("This persons name is : " + product.Location);
                float perc = i / (float)totalCubes;
                i++;
                float x = X;
                float y = Y*2;
                float z = Z;
                GameObject Cenotaph = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);
                Cenotaph.GetComponent<myCubeScript>().setSize(0.5f);
                Cenotaph.GetComponent<myCubeScript>().ratateSpeed = perc;

                var locationNameTransform = Cenotaph.transform.FindChild("LocationName");
                locationNameTransform.gameObject.GetComponent<TextMesh>().text = product.Location;

                var geoLocationNameTransform = Cenotaph.transform.FindChild("GeoLocationName");
                geoLocationNameTransform.gameObject.GetComponent<TextMesh>().text = string.Format("({0}, {1}, {2})", product.X, product.Y, product.Z);
                
                Cenotaph.name = "cube" + count;
                count = count + 1;

            }
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
