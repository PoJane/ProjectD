using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTest : MonoBehaviour
{
    private void Awake()
    {
        EventCenter.Instance.AddEventListener<Dog>("Dog", DogSound);
    }

    private void Start()
    {
        Dog dog = new Dog("wangwang");
        //Debug.Log(dog.GetType());
        //Debug.Log(dog is IAnimal);
        EventCenter.Instance.EventTrigger("Dog", dog);
    }

    public void DogSound(Dog dog)
    {
        dog.Sound();
    }

}

public interface IAnimal
{

}

public class Dog : IAnimal
{
    string name;

    public Dog(string name)
    {
        this.name = name;
    }

    public void Sound()
    {
        Debug.Log(name);
    }
}
