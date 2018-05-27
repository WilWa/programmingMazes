using Mazes.Wpf.Helpers;
using Mazes.Wpf.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Mazes.Wpf.ViewModels
{
    public class MazesViewModel : INotifyPropertyChanged
    {
        private Algorithm _algorithm;
        private int _columns;
        private Geometry _mazeGeometry;
        private int _rows;

        public ObservableCollection<Path> MazePaths = new ObservableCollection<Path>();

        public MazesViewModel()
        {
            Algorithm = Algorithm.Sidewinder;
            Columns = 25;
            Rows = 25;
            CreateExecute();
        }

        public Algorithm Algorithm
        {
            get => _algorithm;
            set
            {
                if (_algorithm == value)
                {
                    return;
                }
                _algorithm = value;
                OnPropertyChanged("Algorithm");
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                if (_columns == value)
                {
                    return;
                }
                _columns = value;
                OnPropertyChanged("Columns");
            }
        }

        public Geometry MazeGeometry
        {
            get => _mazeGeometry;
            set
            {
                if (_mazeGeometry == value)
                {
                    return;
                }
                _mazeGeometry = value;
                OnPropertyChanged("MazeGeometry");
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                if (_rows == value)
                {
                    return;
                }
                _rows = value;
                OnPropertyChanged("Rows");
            }
        }

        #region Create Command

        public ICommand CreateCommand => new CommandHandler(CreateExecute, CanCreateExecute);

        private bool CanCreateExecute()
        {
            return Rows > 0 && Columns > 0;
        }

        private void CreateExecute()
        {
            MazeGeometry = MazeMaker.MakeMazeGeometry(Algorithm, Rows, Columns);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
