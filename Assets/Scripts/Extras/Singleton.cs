using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creo una clase de singleton muy generica para usarla en otras clases, para eso usamosel componente T,
//ya que en unity una clase tambie puede ser un componente
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {

        

            if( _instance == null)
            {
                _instance = FindObjectOfType<T>();
                if( _instance == null)
                {
                    GameObject nuevoGO = new GameObject();
                    _instance = nuevoGO.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance= this as T;
    }
}
