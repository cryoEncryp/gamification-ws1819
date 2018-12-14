using UnityEngine;
using System.Collections;

public class User {
    //age in years, height in cm, weight in kg
    public string gender;
    public int age, height, weight, lvl, totalXP;

    public User(string _gender, int _age, int _height, int _weight, int _lvl = 0, int _totalXP = 0) {
        gender = _gender; age = _age; height = _height; weight = _weight; lvl = _lvl; totalXP = _totalXP;
    }
}
