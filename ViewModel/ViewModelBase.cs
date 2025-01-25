/****************************************************************
 *  Header File: ViewModelBase.cs
 *  Description: Base for the ViewModels
 *
 *    History:
 *     Date        Description
 *     ---------- --------------------------------------
 *    6/22/2020 - Created
 *
 * Copyright (c) DIY Solutions, 2020 (Mike Hankey)
 * 
 * Remarks:
 *  WPF Bindings can be dangerous. The rule of thumb is to always bind to a 
 *  DependencyObject or to a INotifyPropertyChanged object. When you fail to 
 *  do so, WPF will create a strong reference to your binding source (meaning 
 *  the ViewModel) from a static variable, causing a memory leak
 */

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EditableTextBox_Demo.ViewModel
{
    public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public Window ParentWindow { get; set; }

        private bool _supressNoifications;
        public bool SurpressNotifications
        {
            get { return _supressNoifications; }
            set { _supressNoifications = value; }
        }

        public ViewModelBase(Window parent)
        {
            ParentWindow = parent;
        }

        public bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }

        #region INotifyPropertyChanged methods

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>       
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Supresses all notifications...USE CAREFULLY
            if (SurpressNotifications) return;

            // This notifies the UI that anything bound to this property needs to be updated.If the event 
            //  is called with a "null" parameter, then it is the same as notifying that all the properties 
            //  have been updated.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        /// <remarks>
        /// Usage eample: set { SetProperty(ref _firstName, value); }
        /// </remarks>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            // Confirm value has actually changed
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        #endregion
    }
}
