using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace KvantShared.Utils
{
    public class NotifiableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T backingField, T newValue, Expression<Func<T>> propertyExpression)
        {
            PropertyChangedHelper.SetProperty(this, PropertyChanged, ref backingField, newValue, propertyExpression);
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged != null)
                PropertyChangedHelper.RaisePropertyChanged(this, PropertyChanged, propertyExpression);
        }

        protected virtual void RaisePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        protected virtual void RaisePropertyChanged(object sender, string name)
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(name));
        }

        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected virtual void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
