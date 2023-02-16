using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Exercise2
{
    public class Agent : INotifyPropertyChanged
    {
        string id;
        string codeName;
        string speciality;
        string assignment;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public Agent()
        {
        }

        public Agent(string aId, string aName, string aSpeciality, string aAssignment)
        {
            id = aId;
            codeName = aName;
            speciality = aSpeciality;
            assignment = aAssignment;
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                Notify();
            }
        }

        public string CodeName
        {
            get
            {
                return codeName;
            }
            set
            {
                codeName = value;
                Notify();
            }
        }

        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
                Notify();
            }
        }

        public string Assignment
        {
            get
            {
                return assignment;
            }
            set
            {
                assignment = value;
                Notify();
            }
        }
    }
}
