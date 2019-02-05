﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KvantCard.Model;

namespace KvantCard.Vms
{
    public class ContactVm : BaseIdVm
    {
        private ObservableCollection<string> _email;
        public ObservableCollection<string> Email
        {
            get => _email;
            set { SetProperty(ref _email, value, () => Email); }
        }

        private ObservableCollection<string> _phoneNumber;
        public ObservableCollection<string> PhoneNumber
        {
            get => _phoneNumber;
            set { SetProperty(ref _phoneNumber, value, () => PhoneNumber); }
        }

        private ObservableCollection<Address> _address;

        public ObservableCollection<Address> Address
        {
            get => _address;
            set { SetProperty(ref _address, value, () => Address); }
        }
    }
}
