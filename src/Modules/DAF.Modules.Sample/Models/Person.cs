using System;
using System.ComponentModel;

namespace DAF.Modules.Sample.Models
{
    public class Person : INotifyPropertyChanged
    {
        public Person(string firstName, string lastName, int age, DateTime? updateTime)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UpdateTime = updateTime;
        }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            UpdateTime = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public int Age { get; set; }
        public DateTime? UpdateTime { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public override string ToString()
        {
            return $"{Name}'s age {Age}";
        }
    }
}