using UnityEngine;
using System.Collections;

public class User {
    //age in years, height in cm, weight in kg
    public string gender;
    public int age, height, weight;

    public User(string _gender, int _age, int _height, int _weight) {
        gender = _gender; age = _age; height = _height; weight = _weight;
    }
}
