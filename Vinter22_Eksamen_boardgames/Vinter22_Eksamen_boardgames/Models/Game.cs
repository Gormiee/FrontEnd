using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace Vinter22_Eksamen_boardgames.Models
{
    public class Game : BindableBase
    {
        private string _name;
        private string _description;
        private string _releaseyear;
        private int _minplayers;
        private int _maxplayers;
        private double _rating;
        private List<int> _ratings;
        bool _available;

        public Game(string name, string description, string ry, int players, int maxplayers, int rating, Boolean available)
        {
            _name = name;
            _description = description;
            _releaseyear = ry;
            _minplayers = players;
            _maxplayers = maxplayers;
            _rating = 0;
            _available = available;
            _ratings = new List<int>();
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public string ReleaseYear
        {
            get { return _releaseyear; }
            set { SetProperty(ref _releaseyear, value); }
        }
        public int Minplayers
        { get { return _minplayers; }
            set
            {
                SetProperty(ref _minplayers, value);
            }
        }
        public int Maxplayers
        { get { return _maxplayers; }
            set
            {
                SetProperty(ref _maxplayers, value);
            }
        }

        public List<int> Ratings
        {
            get { return _ratings; }
            set
            {
                SetProperty(ref _ratings, value);
            }
        }

        public double Rating
        {
            get { return _rating; }
            set { SetProperty(ref _rating, value);}
        }

        public bool Availability
        {
            get { return _available; }
            set { SetProperty(ref _available, value);}
        }

        
        public void CalcRating(int newRating)
        {
            _ratings.Add(newRating);

            _rating = _ratings.Average();
                     
        }


    }
}
