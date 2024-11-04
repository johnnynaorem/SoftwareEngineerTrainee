using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop
{
    public interface IService
    {
        void UpdatePassword(string username);
        void CreateUser();
        void PreviousOrder(string customerID);
        void OrderSummary();
        void ViewShipperDetail();
        void UserLogin();

    }
}
