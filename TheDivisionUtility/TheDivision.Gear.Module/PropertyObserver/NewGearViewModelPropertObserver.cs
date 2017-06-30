using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheDivisionUtility.TheDivision.Gear.Module.ViewModels;

namespace TheDivisionUtility.TheDivision.Gear.Module.PropertyObserver
{
    public class NewGearViewModelPropertObserver : IPropertyObserver<NewGearViewModel>
    {
        public void BuildObserver(ObserverBuilder<NewGearViewModel> builder)
        {
            builder.Property(x => x.NewGear).OnPropertyChangedCall(x => x.Validate("NewGear"));
            ////builder.Property(x => x.NewGear).OnPropertyChangedCall(x => x.UpdateFirstAndLastFields());
        }
    }
}
