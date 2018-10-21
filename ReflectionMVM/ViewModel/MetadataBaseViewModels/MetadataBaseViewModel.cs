using System;
using System.Collections.ObjectModel;
using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public abstract class MetadataViewModel : BaseViewModel
    {
        #region Constructor

        internal MetadataViewModel()
        {
            Child = new ObservableCollection<MetadataViewModel>
            {
                null
            };

            _isExpanded = false;
            WasBuilt = false;
        }

        #endregion

        #region Properties

        public string FullName => ToString();
        public string Name { get; internal set; }

        public ObservableCollection<MetadataViewModel> Child { get; set; }

        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (value == _isExpanded)
                    return;

                _isExpanded = value;
                if (WasBuilt)
                {
                    return;
                }
                try
                {
                    //TODO: add different ways to display different types
                    BuildMyself();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        #endregion
        
        private bool _isExpanded;
        protected bool WasBuilt;

        protected abstract void BuildMyself();

        public abstract override string ToString();
    }
}