using UnityEngine;

public class SofaConfig : MonoBehaviour
{

    public GameObject Model;


    public void SetMaterial(Material mat)
    {
        gameObject.GetComponent<MeshRenderer>().material = mat;
    }


    public void ChangeMaterial(Material mat)
    {
        Model.GetComponent<MeshRenderer>().material = mat;
    }


    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}
