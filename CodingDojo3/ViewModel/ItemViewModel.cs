using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using System;

namespace CodingDojo3.ViewModel
{
    public class ItemViewModel : ViewModelBase
    {
        private ItemBase baseItem;

        public int ID { get { return this.baseItem.Id; } }
        public string Name { get { return this.baseItem.Name; } set { baseItem.Name = value; RaisePropertyChanged(); } }

        public string Description { get { return this.baseItem.Description; } set { baseItem.Description = value; RaisePropertyChanged(); } }

        public string Room { get { return this.baseItem.Room; } set { baseItem.Room = value; RaisePropertyChanged(); } }
        public int PosX { get { return this.baseItem.PosX; } set { baseItem.PosX = value; RaisePropertyChanged(); } }
        public int PosY { get { return this.baseItem.PosY; } set { baseItem.PosY = value; RaisePropertyChanged(); } }

        // soll Shared.dll abstrahieren

        public string ValueType
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValueType.Name;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValueType.Name;
                else
                    throw new NotImplementedException();
            }

        }


        public Type ItemType
        {
            get
            {
                if (baseItem is ISensor) return typeof(ISensor);
                else if (baseItem is IActuator) return typeof(IActuator);
                else throw new NotImplementedException();
            }
        }


        public string Mode
        {
            get
            {
                if (baseItem is ISensor) return (baseItem as BaseSensor).SensorMode.ToString();
                if (baseItem is IActuator) return (baseItem as BaseActuator).ActuatorMode.ToString();
                else return null;
            }
            set
            {
                if (baseItem is ISensor)
                    (baseItem as BaseSensor).SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value, false);
                if (baseItem is IActuator)
                    (baseItem as BaseActuator).ActuatorMode = (ModeType)Enum.Parse(typeof(ModeType), value, false);

                RaisePropertyChanged();
            }
        }

        public object Value
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValue;
                else if (baseItem is IActuator)
                    return (baseItem as BaseActuator).ActuatorValue;
                else
                    throw new NotImplementedException();
            }

            set
            {
                if (baseItem is ISensor) (baseItem as BaseSensor).SensorValue = value;
                else if (baseItem is IActuator) (baseItem as BaseActuator).ActuatorValue = value;
                else
                    throw new NotImplementedException();
                RaisePropertyChanged();

            }
        }

        public ItemViewModel(ISensor sensor)
        {
            baseItem = sensor as ItemBase;
        }

        public ItemViewModel(IActuator actuator)
        {
            baseItem = actuator as ItemBase;
        }


    }
}