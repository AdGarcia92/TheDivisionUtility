using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using StructureMap;
using StructureMap.Attributes;

namespace TheDivisionUtility.TheDivision.Gear.Module
{
    public abstract class ModuleBindableBase : BindableBase, IModule, IDisposable
    {
        private bool _isDisposed = false;

        private IRegionManager _regionManager;

        ~ModuleBindableBase()
        {
            Dispose(false);
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        [SetterProperty]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRegionManager RegionManager
        {
            set
            {
                if (_regionManager == null)
                {
                    _regionManager = value;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDispose(bool isSafeToUsePrivateReferences)
        {
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            OnDispose(disposing);

            _isDisposed = true;
        }
    }
}
